using MediatR;

namespace Bookshelf.Application.Genres.Commands.UpdateGenre;

public record UpdateGenreCommand(Guid Id, string Name) : IRequest;