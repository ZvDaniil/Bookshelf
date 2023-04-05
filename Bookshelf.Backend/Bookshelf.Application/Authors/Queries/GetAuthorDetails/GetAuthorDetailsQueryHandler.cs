using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Authors.Queries.GetAuthorDetails;

internal class GetAuthorDetailsQueryHandler : IRequestHandler<GetAuthorDetailsQuery, AuthorDetailsVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorDetailsQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<AuthorDetailsVm> Handle(GetAuthorDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Authors
            .Include(author => author.Books)
            .ThenInclude(book => book.Genres)
            .AsNoTracking()
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        return _mapper.Map<AuthorDetailsVm>(entity);
    }
}
