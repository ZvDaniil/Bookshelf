using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Authors.Queries.GetAuthorDetails;

internal class GetAuthorDetailsQueryHandler : IRequestHandler<GetAuthorDetailsQuery, AuthorDetailsVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorDetailsQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<AuthorDetailsVm> Handle(GetAuthorDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
            .AsNoTracking()
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        return _mapper.Map<AuthorDetailsVm>(entity);
    }
}
