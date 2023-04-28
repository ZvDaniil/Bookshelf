using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Books.Commands.AddBookGenre;

internal sealed class AddBookGenreCommandHandler : IRequestHandler<AddBookGenreCommand, Guid>
{
    private readonly IBookshelfDbContext _dbContext;

    public AddBookGenreCommandHandler(IBookshelfDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Guid> Handle(AddBookGenreCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(book => book.Id == request.BookId, cancellationToken);

        if (book is null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var genre = await _dbContext.Genres
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(genre => genre.Id == request.GenreId, cancellationToken);

        if (genre is null)
        {
            throw new NotFoundException(nameof(Genre), request.GenreId);
        }

        book.Genres ??= new List<Genre>();
        if (book.Genres.Any(genre => genre.Id == request.GenreId))
        {
            var message = $"Book with id {request.BookId} already contains genre with id {request.GenreId}";
            throw new InvalidOperationException(message);
        }

        book.Genres.Add(genre);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
