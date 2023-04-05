using MediatR;

namespace Bookshelf.Application.Genres.Commands.DeleteGenre;

public record DeleteGenreCommand(Guid Id) : IRequest;
