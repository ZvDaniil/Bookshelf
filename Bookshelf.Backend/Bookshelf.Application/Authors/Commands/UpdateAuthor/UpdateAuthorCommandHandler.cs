using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public UpdateAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}