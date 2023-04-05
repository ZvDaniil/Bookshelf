using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;

namespace Bookshelf.Application.Interfaces;

public interface IBookshelfDbContext
{
    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}