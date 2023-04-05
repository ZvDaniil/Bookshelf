﻿using MediatR;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(string FirstName, string LastName) : IRequest<Guid>;
