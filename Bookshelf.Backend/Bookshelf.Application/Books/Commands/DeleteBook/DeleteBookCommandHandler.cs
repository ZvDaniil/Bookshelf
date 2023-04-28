using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Books.Commands.DeleteBook;

internal sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public DeleteBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        _dbContext.Books.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}