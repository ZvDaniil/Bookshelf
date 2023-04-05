using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Genres.Models;

namespace Bookshelf.Application.Genres.Queries.GetGenreList;

internal class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, GenreListVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreListQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GenreListVm> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
    {
        var genreQuery = await _dbContext.Genres
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .ProjectTo<GenreLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GenreListVm { Genres = genreQuery };
    }
}