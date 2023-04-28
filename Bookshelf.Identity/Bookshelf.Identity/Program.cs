using Bookshelf.Identity;
using Bookshelf.Identity.Data;
using Bookshelf.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occured while app initialization");
    }
}

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<AuthDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("Sqlite")));

    services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();

    services.AddIdentityServer()
        .AddAspNetIdentity<AppUser>()
        .AddInMemoryApiResources(Configuration.ApiResources)
        .AddInMemoryIdentityResources(Configuration.IdentityResources)
        .AddInMemoryApiScopes(Configuration.ApiScopes)
        .AddInMemoryClients(Configuration.Clients)
        .AddDeveloperSigningCredential();

    services.ConfigureApplicationCookie(configure =>
    {
        configure.LoginPath = "/Auth/Login";
        configure.LogoutPath = "/Auth/Logout";
        configure.Cookie.Name = "Bookshelf.Identity.Cookie";
    });

    services.AddControllersWithViews();
}