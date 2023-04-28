using MediatR;

namespace Bookshelf.Application.Books.Commands.DeleteBookGenre;

public record DeleteBookGenreCommand(Guid BookId, Guid GenreId) : IRequest;
