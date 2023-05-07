using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(deleteGenreCommand => deleteGenreCommand.Id).NotEqual(Guid.Empty);
    }
}