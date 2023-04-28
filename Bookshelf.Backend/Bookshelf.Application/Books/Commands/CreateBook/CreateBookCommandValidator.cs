using FluentValidation;

namespace Bookshelf.Application.Books.Commands.CreateBook;

internal class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(createBookCommand => createBookCommand.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(createBookCommand => createBookCommand.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(createBookCommand => createBookCommand.AgeRestriction)
            .InclusiveBetween(0, 18);

        RuleFor(createBookCommand => createBookCommand.AuthorId)
           .NotEqual(Guid.Empty);

        RuleFor(createBookCommand => createBookCommand.DatePublished)
            .NotEqual(default(DateTime))
            .Must(date => date.Date <= DateTime.Today);

        RuleFor(createBookCommand => createBookCommand.Pages)
            .GreaterThan(0);

        RuleFor(createBookCommand => createBookCommand.Price)
            .GreaterThan(0);

        RuleFor(createBookCommand => createBookCommand.ISBN)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(createBookCommand => createBookCommand.GenreIds)
            .NotEmpty();
    }
}