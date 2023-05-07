using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Books.Commands.UpdateBook;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public UpdateBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (book is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        var author = await _dbContext.Authors
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(author => author.Id == request.AuthorId, cancellationToken);

        if (author is null)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }

        book.Author = author;
        book.Title = request.Title;
        book.Description = request.Description;
        book.Pages = request.Pages;
        book.Price = request.Price;
        book.ISBN = request.ISBN;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}