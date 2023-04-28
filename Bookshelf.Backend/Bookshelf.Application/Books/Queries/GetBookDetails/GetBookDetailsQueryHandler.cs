using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Common.Extensions;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Genres.Models;
using Bookshelf.Domain.Base;

namespace Bookshelf.Application.Books.Queries.GetBookDetails;

internal class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDetailsVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookDetailsQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext, IMapper mapper) =>
        (_currentUserService, _dbContext, _mapper) = (currentUserService, dbContext, mapper);

    public async Task<BookDetailsVm> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .AsNoTracking()
            .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        return _mapper.Map<BookDetailsVm>(entity);
    }
}