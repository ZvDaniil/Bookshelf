using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Domain;

namespace Bookshelf.Application.Books.Commands.HideBook;

internal class HideBookCommandHandler : IRequestHandler<HideBookCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public HideBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(HideBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .IgnoreAutoIncludes()
            .Include(book => book.Reviews)
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.Visible = false;
        if (entity.Reviews is not null)
        {
            await HideRelatedReviewsAsync(entity.Id, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task HideRelatedReviewsAsync(Guid bookId, CancellationToken cancellationToken) =>
        await _dbContext.Reviews
                .IgnoreAutoIncludes()
                .Include(book => book.Book)
                .Where(review => review.BookId == bookId)
                .ExecuteUpdateAsync(s => s.SetProperty(r => r.Visible, false), cancellationToken);
}
