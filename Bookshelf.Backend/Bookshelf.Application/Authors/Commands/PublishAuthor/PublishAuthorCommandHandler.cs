using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Authors.Commands.PublishAuthor;

internal sealed class PublishAuthorCommandHandler : IRequestHandler<PublishAuthorCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public PublishAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(PublishAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(author => author.Id == request.Id && !author.Visible, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        entity.Visible = true;
        if (request.PublishBooks)
        {
            await PublishRelatedBooksAsync(entity.Id, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishRelatedBooksAsync(Guid authorId, CancellationToken cancellationToken) =>
        await _dbContext.Books
                .IgnoreQueryFilters()
                .IgnoreAutoIncludes()
                .Include(book => book.Author)
                .Where(book => book.AuthorId == authorId && !book.Visible)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.Visible, true), cancellationToken);
}