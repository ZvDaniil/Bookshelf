using MediatR;

namespace Bookshelf.Application.Genres.Commands.HideGenre;

public record HideGenreCommand(Guid Id) : IRequest;
