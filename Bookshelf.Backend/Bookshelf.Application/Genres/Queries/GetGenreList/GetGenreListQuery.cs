using MediatR;
using Bookshelf.Application.Genres.Models;

namespace Bookshelf.Application.Genres.Queries.GetGenreList;

public record GetGenreListQuery : IRequest<GenreListVm>;