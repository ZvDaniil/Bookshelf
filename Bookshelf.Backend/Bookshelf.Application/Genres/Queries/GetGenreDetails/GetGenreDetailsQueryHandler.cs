using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Genres.Models;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Genres.Queries.GetGenreDetails;

internal sealed class GetGenreDetailsQueryHandler : IRequestHandler<GetGenreDetailsQuery, GenreDetailsVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailsQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<GenreDetailsVm> Handle(GetGenreDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres
            .IgnoreAutoIncludes()
            .Include(genre => genre.Books!)
            .ThenInclude(book => book.Author)
            .AsNoTracking()
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .FirstOrDefaultAsync(genre => genre.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        return _mapper.Map<GenreDetailsVm>(entity);
    }
}
