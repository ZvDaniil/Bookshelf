using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Domain.Interfaces;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Reviews.Commands.PublishReview;

internal sealed class PublishReviewCommandHandler : IRequestHandler<PublishReviewCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public PublishReviewCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(PublishReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Reviews
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(review => review.Id == request.Id && !review.Visible, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Review), request.Id);
        }

        entity.Visible = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
