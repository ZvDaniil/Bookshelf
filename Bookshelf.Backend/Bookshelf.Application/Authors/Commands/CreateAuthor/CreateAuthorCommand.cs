using MediatR;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(string FirstName, string LastName, bool Visible = false) : IRequest<Guid>;