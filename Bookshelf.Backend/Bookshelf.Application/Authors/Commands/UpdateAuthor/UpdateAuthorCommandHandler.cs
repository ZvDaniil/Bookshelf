using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public UpdateAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(author => author.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Visible = request.Visible;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}