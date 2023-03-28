using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;

namespace Bookshelf.Application.Interfaces;

public interface IBookshelfDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Review> Reviews { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
