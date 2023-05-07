using MediatR;

namespace Bookshelf.Application.Books.Commands.PublishBook;

public record PublishBookCommand(Guid Id) : IRequest<Guid>;
