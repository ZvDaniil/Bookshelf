using MediatR;
using Bookshelf.Application.Books.Models;

namespace Bookshelf.Application.Books.Queries.GetBookList;

public record GetBookListQuery : IRequest<BookListVm>;