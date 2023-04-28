using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Common.Extensions;
using Bookshelf.Domain.Base;

namespace Bookshelf.Application.Authors.Queries.GetAuthorList;

internal sealed class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorListQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authorsQuery = await _dbContext.Authors
            .AsNoTracking()
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .ProjectTo<AuthorLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AuthorListVm { Authors = authorsQuery };
    }
}