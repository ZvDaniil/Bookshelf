using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Genres.Models;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Genres.Queries.GetGenreDetails;

internal sealed class GetGenreDetailsQueryHandler : IRequestHandler<GetGenreDetailsQuery, GenreDetailsVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailsQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GenreDetailsVm> Handle(GetGenreDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres
            .Include(genre => genre.Books)
            .AsNoTracking()
            .FirstOrDefaultAsync(genre => genre.Id == request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(nameof(Genre), request.Id);

        return _mapper.Map<GenreDetailsVm>(entity);
    }
}
