using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bookshelf.Application.Interfaces;

public interface IBookshelfDbContext
{
    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Review> Reviews { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}