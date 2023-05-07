using AutoMapper;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Reviews.Models;
using Bookshelf.Domain;
using Bookshelf.Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Reviews.Queries.GetReviewDetails;

internal sealed class GetReviewDetailsQueryHandler : IRequestHandler<GetReviewDetailsQuery, ReviewDetailsVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReviewDetailsQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<ReviewDetailsVm> Handle(GetReviewDetailsQuery request, CancellationToken cancellationToken)
    {
        var isAdmin = _currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName);
        var entity = await _dbContext.Reviews
            .IgnoreAutoIncludes()
            .Include(review => review.Book)
            .IgnoreQueryFilters(isAdmin)
            .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);

        if (entity is null || (!isAdmin && entity.UserId != _currentUserService.UserId))
        {
            throw new NotFoundException(nameof(Review), request.Id);
        }

        return _mapper.Map<ReviewDetailsVm>(entity);
    }
}
