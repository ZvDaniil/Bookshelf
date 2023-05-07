using FluentValidation;

namespace Bookshelf.Application.Books.Commands.HideBook;

public class HideBookCommandValidator : AbstractValidator<HideBookCommand>
{
    public HideBookCommandValidator()
    {
        RuleFor(hideBookCommand => hideBookCommand.Id).NotEqual(Guid.Empty);
    }
}
