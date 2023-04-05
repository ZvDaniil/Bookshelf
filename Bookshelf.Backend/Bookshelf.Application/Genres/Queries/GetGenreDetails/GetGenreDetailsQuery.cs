using MediatR;
using Bookshelf.Application.Genres.Models;

namespace Bookshelf.Application.Genres.Queries.GetGenreDetails;

public record GetGenreDetailsQuery(Guid Id) : IRequest<GenreDetailsVm>;
