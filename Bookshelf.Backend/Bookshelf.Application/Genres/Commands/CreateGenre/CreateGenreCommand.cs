using MediatR;

namespace Bookshelf.Application.Genres.Commands.CreateGenre;

public record CreateGenreCommand(string Name, bool Visible) : IRequest<Guid>;