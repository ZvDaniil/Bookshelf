using MediatR;
using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Authors.Queries.GetAuthorList;

public record GetAuthorListQuery : IRequest<AuthorListVm>;
