using System.Reflection;

using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Configurations;

using Bookshelf.Persistence;
using Bookshelf.Persistence.Configurations;

using Bookshelf.Api;
using Bookshelf.Api.Services;
using Bookshelf.Api.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<BookshelfDbContext>();
    DbInitializer.Initialize(context);

    var apiVersionDescriptionProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
    Configure(app, apiVersionDescriptionProvider);

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "An error occured while app initialization");
}
finally
{
    Thread.Sleep(5000);
    Log.CloseAndFlush();
}

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IBookshelfDbContext).Assembly));
    });

    services.AddApplication();
    services.AddPersistence(configuration);

    services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

    services.AddSwaggerGen();
    services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddHttpContextAccessor();
    services.AddSingleton<ICurrentUserService, CurrentUserService>();

    services.AddControllers();

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = "https://localhost:7111/";
            options.Audience = "BookshelfWebAPI";
            options.RequireHttpsMetadata = false;
        });
}

static void Configure(WebApplication app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            config.RoutePrefix = string.Empty;
            config.OAuthClientId("api-swagger");
            config.OAuthScopes("profile", "openid", "BookshelfWebAPI", "roles");
            config.OAuthUsePkce();
        }
    });

    app.UseCustomExceptionHandler();
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseApiVersioning();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}