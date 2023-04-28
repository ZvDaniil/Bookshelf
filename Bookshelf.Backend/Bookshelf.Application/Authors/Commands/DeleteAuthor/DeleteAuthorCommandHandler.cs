using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Authors.Commands.DeleteAuthor;

internal sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public DeleteAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(author => author.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        _dbContext.Authors.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}