using System.Reflection;

using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Configurations;

using Bookshelf.Persistence;
using Bookshelf.Persistence.Configurations;

using Bookshelf.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<BookshelfDbContext>();
    DbInitializer.Initialize(context);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IBookshelfDbContext).Assembly));
    });

    services.AddApplication();
    services.AddPersistence(configuration);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

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
}