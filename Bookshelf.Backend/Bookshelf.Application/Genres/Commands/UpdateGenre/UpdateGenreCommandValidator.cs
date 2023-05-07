using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(updateGenreCommand => updateGenreCommand.Id)
            .NotEqual(Guid.Empty);

        RuleFor(updateGenreCommand => updateGenreCommand.Name)
            .Length(5, 50);

        RuleFor(updateGenreCommand => updateGenreCommand.Description)
            .MaximumLength(1024);
    }
}
