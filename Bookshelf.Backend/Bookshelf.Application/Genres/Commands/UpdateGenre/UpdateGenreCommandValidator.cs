using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.UpdateGenre;

internal class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(updateGenreCommand => updateGenreCommand.Id)
            .NotEqual(Guid.Empty);

        RuleFor(updateGenreCommand => updateGenreCommand.Name)
            .MinimumLength(4)
            .MaximumLength(50);
    }
}
