using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Books.Commands.UpdateBook;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public UpdateBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Pages = request.Pages;
        entity.Price = request.Price;
        entity.ISBN = request.ISBN;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}