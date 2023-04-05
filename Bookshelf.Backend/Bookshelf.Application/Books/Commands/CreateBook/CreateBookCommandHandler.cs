using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Books.Commands.CreateBook;

internal sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookshelfDbContext _dbContext;

    public CreateBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FindAsync(new object[] { request.AuthorId }, cancellationToken);
        if (author is null)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }

        var genres = await _dbContext.Genres.Where(g => request.GenreIds.Contains(g.Id)).ToListAsync(cancellationToken);
        if (genres.Count != request.GenreIds.Count)
        {
            var invalidGenreIds = request.GenreIds.Except(genres.Select(g => g.Id));
            throw new NotFoundException(nameof(Genre), invalidGenreIds);
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            AgeRestriction = request.AgeRestriction,
            DatePublished = request.DatePublished,
            Pages = request.Pages,
            Price = request.Price,
            ISBN = request.ISBN,
            AuthorId = request.AuthorId,
            Author = author,
            Genres = genres,
            Reviews = new List<Review>(),
        };

        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
