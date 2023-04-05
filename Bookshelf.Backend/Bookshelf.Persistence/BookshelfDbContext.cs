using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Persistence;

public class BookshelfDbContext : DbContext, IBookshelfDbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    public BookshelfDbContext(DbContextOptions<BookshelfDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(BookshelfDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
