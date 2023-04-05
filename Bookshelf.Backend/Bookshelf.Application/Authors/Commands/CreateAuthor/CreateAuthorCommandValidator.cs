using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

internal class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(createAuthorCommand => createAuthorCommand.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(createAuthorCommand => createAuthorCommand.LastName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}
