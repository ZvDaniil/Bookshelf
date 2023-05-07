using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.HideAuthor;

public class HideAuthorCommandValidator : AbstractValidator<HideAuthorCommand>
{
    public HideAuthorCommandValidator()
    {
        RuleFor(hideAuthorCommand => hideAuthorCommand.Id).NotEqual(Guid.Empty);
    }
}
