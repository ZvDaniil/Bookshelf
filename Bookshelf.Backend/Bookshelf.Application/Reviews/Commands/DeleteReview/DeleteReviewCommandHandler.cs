using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;
using Bookshelf.Application.Interfaces;
using Bookshelf.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain.Base;

namespace Bookshelf.Application.Reviews.Commands.DeleteReview;

internal sealed class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;

    public DeleteReviewCommandHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext) =>
        (_currentUserService, _dbContext) = (currentUserService, dbContext);

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var isAdmin = _currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName);
        var entity = await _dbContext.Reviews
            .AsNoTracking()
            .IgnoreQueryFilters(isAdmin)
            .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);

        if (entity is null || (!isAdmin && entity.UserId != _currentUserService.UserId))
        {
            throw new NotFoundException(nameof(Review), request.Id);
        }

        _dbContext.Reviews.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
