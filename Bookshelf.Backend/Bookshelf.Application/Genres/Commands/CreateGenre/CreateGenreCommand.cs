using MediatR;

namespace Bookshelf.Application.Genres.Commands.CreateGenre;

public record CreateGenreCommand(string Name, string Description, bool Visible) : IRequest<Guid>;