using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

internal class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(createAuthorCommand => createAuthorCommand.FirstName)
            .NotEmpty()
            .Length(3, 50);

        RuleFor(createAuthorCommand => createAuthorCommand.LastName)
            .NotEmpty()
            .Length(3, 50);
    }
}
