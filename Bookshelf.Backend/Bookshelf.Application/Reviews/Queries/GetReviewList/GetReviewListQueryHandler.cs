using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Reviews.Models;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Reviews.Queries.GetReviewList;

internal sealed class GetReviewListQueryHandler : IRequestHandler<GetReviewListQuery, ReviewListVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReviewListQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<ReviewListVm> Handle(GetReviewListQuery request, CancellationToken cancellationToken)
    {
        var reviewsQuery = await _dbContext.Reviews
            .IgnoreAutoIncludes()
            .Include(review => review.Book)
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .Where(r => r.BookId == request.BookId)
            .ProjectTo<ReviewLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ReviewListVm { Reviews = reviewsQuery };
    }
}
