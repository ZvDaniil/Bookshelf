using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Authors.Queries.GetAuthorList;

internal sealed class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorListQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authorQuery = await _dbContext.Authors
            .AsNoTracking()
            .ProjectTo<AuthorLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AuthorListVm { Authors = authorQuery };
    }
}