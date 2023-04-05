using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Books.Queries.GetBookDetails;

internal class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDetailsVm>
{
    private readonly IBookshelfDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookDetailsQueryHandler(IBookshelfDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<BookDetailsVm> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .AsNoTracking()
            .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        return _mapper.Map<BookDetailsVm>(entity);
    }
}