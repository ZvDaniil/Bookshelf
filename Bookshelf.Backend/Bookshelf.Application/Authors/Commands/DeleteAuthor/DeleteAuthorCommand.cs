using MediatR;

namespace Bookshelf.Application.Authors.Commands.DeleteAuthor;

public record DeleteAuthorCommand(Guid Id) : IRequest;
