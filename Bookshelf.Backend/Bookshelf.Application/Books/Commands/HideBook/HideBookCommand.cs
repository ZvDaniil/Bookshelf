using MediatR;

namespace Bookshelf.Application.Books.Commands.HideBook;

public record HideBookCommand(Guid Id) : IRequest;
