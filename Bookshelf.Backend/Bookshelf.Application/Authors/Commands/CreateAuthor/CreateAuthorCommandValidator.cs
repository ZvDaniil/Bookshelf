using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(createAuthorCommand => createAuthorCommand.FirstName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(createAuthorCommand => createAuthorCommand.LastName)
            .NotEmpty()
            .Length(2, 50);
    }
}
