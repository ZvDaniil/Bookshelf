using FluentValidation;

namespace Bookshelf.Application.Books.Commands.UpdateBook;

internal class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(updateBookCommand => updateBookCommand.Id)
            .NotEqual(Guid.Empty);

        RuleFor(updateBookCommand => updateBookCommand.Title)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(updateBookCommand => updateBookCommand.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(updateBookCommand => updateBookCommand.AuthorId)
            .NotEqual(Guid.Empty);

        RuleFor(updateBookCommand => updateBookCommand.AgeRestriction)
            .InclusiveBetween(0, 18);

        RuleFor(updateBookCommand => updateBookCommand.DatePublished)
            .NotEqual(default(DateTime))
            .Must(date => date.Date <= DateTime.Today);

        RuleFor(updateBookCommand => updateBookCommand.Pages)
            .GreaterThan(0);

        RuleFor(updateBookCommand => updateBookCommand.Price)
            .GreaterThan(0);

        RuleFor(updateBookCommand => updateBookCommand.ISBN)
            .NotEmpty()
            .MaximumLength(20);
    }
}
