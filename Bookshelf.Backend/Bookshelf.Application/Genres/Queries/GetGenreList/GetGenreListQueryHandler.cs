using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Genres.Models;
using Bookshelf.Application.Common.Extensions;
using Bookshelf.Domain.Base;

namespace Bookshelf.Application.Genres.Queries.GetGenreList;

internal class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, GenreListVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreListQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<GenreListVm> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
    {
        var genreQuery = await _dbContext.Genres
            .AsNoTracking()
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .ProjectTo<GenreLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GenreListVm { Genres = genreQuery };
    }
}