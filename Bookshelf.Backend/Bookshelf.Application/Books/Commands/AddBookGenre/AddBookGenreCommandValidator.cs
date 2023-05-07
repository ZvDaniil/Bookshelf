using FluentValidation;

namespace Bookshelf.Application.Books.Commands.AddBookGenre;

public class AddBookGenreCommandValidator : AbstractValidator<AddBookGenreCommand>
{
    public AddBookGenreCommandValidator()
    {
        RuleFor(addBookGenreCommand => addBookGenreCommand.BookId).NotEqual(Guid.Empty);
        RuleFor(addBookGenreCommand => addBookGenreCommand.GenreId).NotEqual(Guid.Empty);
    }
}