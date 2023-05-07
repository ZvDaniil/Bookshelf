using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Reviews.Commands.HideReview;

internal sealed class HideReviewCommandHandler : IRequestHandler<HideReviewCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public HideReviewCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(HideReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Reviews
            .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Review), request.Id);
        }

        entity.Visible = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
