using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(updateAuthorCommand => updateAuthorCommand.Id)
            .NotEqual(Guid.Empty);

        RuleFor(updateAuthorCommand => updateAuthorCommand.FirstName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(UpdateAuthorCommand => UpdateAuthorCommand.LastName)
            .NotEmpty()
            .Length(2, 50);
    }
}
