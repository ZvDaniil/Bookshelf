using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Books.Models;

namespace Bookshelf.Application.Books.Queries.GetBookList;

internal sealed class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, BookListVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookListQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<BookListVm> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var bookQuery = await _dbContext.Books
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .Include(book => book.Reviews)
            .AsNoTracking()
            .ProjectTo<BookLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new BookListVm { Books = bookQuery };
    }
}