using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Reviews.Commands.UpdateReview;

internal sealed class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;

    public UpdateReviewCommandHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext) =>
        (_currentUserService, _dbContext) = (currentUserService, dbContext);

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var isAdmin = _currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName);
        var entity = await _dbContext.Reviews
            .IgnoreQueryFilters(isAdmin)
            .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Review), request.Id);
        }

        entity.Rating = request.Rating;
        entity.Content = request.Content;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
