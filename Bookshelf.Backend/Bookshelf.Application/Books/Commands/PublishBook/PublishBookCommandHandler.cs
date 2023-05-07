using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Books.Commands.PublishBook;

public class PublishBookCommandHandler : IRequestHandler<PublishBookCommand, Guid>
{
    private readonly IBookshelfDbContext _dbContext;

    public PublishBookCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(PublishBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .IgnoreQueryFilters()
            .Include(book => book.Reviews)
            .FirstOrDefaultAsync(book => book.Id == request.Id && !book.Visible, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        if (!entity.Author.Visible)
        {
            throw new CannotPublishException(nameof(Book), entity.Id, ErrorMessages.HiddenAuthor);
        }

        if (entity.Genres is null || !entity.Genres.Any(genre => genre.Visible))
        {
            throw new CannotPublishException(nameof(Book), entity.Id, ErrorMessages.NoVisibleGenres);
        }

        entity.Visible = true;
        if (entity.Reviews is not null)
        {

        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task PublishRelatedReviewsAsync(Guid bookId, CancellationToken cancellationToken) =>
        await _dbContext.Reviews
                .IgnoreAutoIncludes()
                .IgnoreQueryFilters()
                .Include(book => book.Book)
                .Where(review => review.BookId == bookId && !review.Visible)
                .ExecuteUpdateAsync(s => s.SetProperty(r => r.Visible, true), cancellationToken);
}