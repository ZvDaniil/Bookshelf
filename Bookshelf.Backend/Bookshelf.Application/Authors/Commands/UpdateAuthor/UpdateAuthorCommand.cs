using MediatR;

namespace Bookshelf.Application.Authors.Commands.UpdateAuthor;

public record UpdateAuthorCommand(Guid Id, string FirstName, string LastName, bool Visible) : IRequest;
