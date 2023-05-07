using MediatR;

namespace Bookshelf.Application.Authors.Commands.HideAuthor;

public record HideAuthorCommand(Guid Id) : IRequest;
