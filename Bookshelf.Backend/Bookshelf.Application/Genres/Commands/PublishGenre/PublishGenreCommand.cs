using MediatR;

namespace Bookshelf.Application.Genres.Commands.PublishGenre;

public record PublishGenreCommand(Guid Id) : IRequest;
