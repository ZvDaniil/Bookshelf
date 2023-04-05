using MediatR;
using Bookshelf.Application.Books.Models;

namespace Bookshelf.Application.Books.Queries.GetBookDetails;

public record GetBookDetailsQuery(Guid Id) : IRequest<BookDetailsVm>;
