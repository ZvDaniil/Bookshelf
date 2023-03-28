using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Persistence.EntityTypeConfigurations;

namespace Bookshelf.Persistence;

public class BookshelfDbContext : DbContext, IBookshelfDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public BookshelfDbContext(DbContextOptions<BookshelfDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new GenreConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());

        base.OnModelCreating(builder);
    }
}
