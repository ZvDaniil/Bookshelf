using MediatR;

namespace Bookshelf.Application.Books.Commands.DeleteBook;

public record DeleteBookCommand(Guid Id) : IRequest;
