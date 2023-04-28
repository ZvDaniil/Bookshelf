using MediatR;

namespace Bookshelf.Application.Books.Commands.AddBookGenre;

public record AddBookGenreCommand(Guid BookId, Guid GenreId) : IRequest<Guid>;