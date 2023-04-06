using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Persistence.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookshelfDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Sqlite"));
        });

        services.AddScoped<IBookshelfDbContext>(provider => provider.GetRequiredService<BookshelfDbContext>());

        return services;
    }
}