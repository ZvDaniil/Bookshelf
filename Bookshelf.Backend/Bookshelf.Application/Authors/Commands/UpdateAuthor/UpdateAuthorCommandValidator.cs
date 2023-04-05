using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.UpdateAuthor;

internal class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(updateAuthorCommand => updateAuthorCommand.Id)
            .NotEqual(Guid.Empty);

        RuleFor(updateAuthorCommand => updateAuthorCommand.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(UpdateAuthorCommand => UpdateAuthorCommand.LastName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}
