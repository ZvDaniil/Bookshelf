using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.DeleteGenre;

internal class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(deleteGenreCommand => deleteGenreCommand.Id).NotEqual(Guid.Empty);
    }
}