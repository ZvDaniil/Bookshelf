using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Authors.Commands.HideAuthor;

internal sealed class HideAuthorCommandHandler : IRequestHandler<HideAuthorCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public HideAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(HideAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
             .FirstOrDefaultAsync(author => author.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        entity.Visible = false;
        await HideRelatedBooksAsync(request.Id, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task HideRelatedBooksAsync(Guid authorId, CancellationToken cancellationToken) =>
        await _dbContext.Books
                .IgnoreAutoIncludes()
                .Include(book => book.Author)
                .Where(book => book.AuthorId == authorId)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.Visible, false), cancellationToken);
}
