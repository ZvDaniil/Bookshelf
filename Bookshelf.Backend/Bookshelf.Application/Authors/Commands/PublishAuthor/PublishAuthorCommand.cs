using MediatR;

namespace Bookshelf.Application.Authors.Commands.PublishAuthor;

public record PublishAuthorCommand(Guid Id, bool PublishBooks) : IRequest;