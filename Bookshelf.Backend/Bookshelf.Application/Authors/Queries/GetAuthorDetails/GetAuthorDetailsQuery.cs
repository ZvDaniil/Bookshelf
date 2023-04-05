using MediatR;
using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Authors.Queries.GetAuthorDetails;

public record GetAuthorDetailsQuery(Guid Id) : IRequest<AuthorDetailsVm>;
