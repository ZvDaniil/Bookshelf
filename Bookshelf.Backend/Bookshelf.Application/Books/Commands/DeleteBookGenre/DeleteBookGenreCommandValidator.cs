using FluentValidation;

namespace Bookshelf.Application.Books.Commands.DeleteBookGenre;

internal class DeleteBookGenreCommandValidator : AbstractValidator<DeleteBookGenreCommand>
{
    public DeleteBookGenreCommandValidator()
    {
        RuleFor(deleteBookGenreCommand => deleteBookGenreCommand.BookId).NotEqual(Guid.Empty);
        RuleFor(deleteBookGenreCommand => deleteBookGenreCommand.GenreId).NotEqual(Guid.Empty);
    }
}
