using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Interfaces;
using Bookshelf.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Bookshelf.Application.Books.Commands.DeleteBookGenre;

internal sealed class DeleteBookGenreCommandHandler : IRequestHandler<DeleteBookGenreCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public DeleteBookGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(DeleteBookGenreCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(book => book.Id == request.BookId, cancellationToken);

        if (book is null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var genre = book.Genres?.FirstOrDefault(genre => genre.Id == request.GenreId);
        if (genre is null)
        {
            var message = $"Book with id {request.BookId} does not contain genre with id {request.GenreId}";
            throw new InvalidOperationException(message);
        }

        book.Genres!.Remove(genre);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

