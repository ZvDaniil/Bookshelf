using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain.Base;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Common.Extensions;

namespace Bookshelf.Application.Books.Queries.GetBookList;

internal sealed class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, BookListVm>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;

    public GetBookListQueryHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext) =>
        (_currentUserService, _dbContext) = (currentUserService, dbContext);

    public async Task<BookListVm> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var booksQuery = await _dbContext.Books
           .Include(book => book.Reviews)
           .AsNoTracking()
           .IgnoreQueryFilters(_currentUserService.CurrentUserIsInRole(AppData.SystemAdministratorRoleName))
           .Select(book => new BookLookupDto
           {
               Id = book.Id,
               Title = book.Title,

               Author = new AuthorLookupDto
               {
                   Id = book.Author.Id,
                   FullName = $"{book.Author.FirstName} {book.Author.LastName}",
                   Visible = book.Author.Visible
               },

               ReviewsCount = book.Reviews != null
                        ? book.Reviews.Count
                        : 0,

               AverageRating = book.Reviews != null && book.Reviews.Any()
                        ? book.Reviews.Average(r => r.Rating)
                        : 0,

               Visible = book.Visible
           })
           .ToListAsync(cancellationToken);

        return new BookListVm { Books = booksQuery };
    }
}