using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.HideGenre;

public class HideGenreCommandValidator : AbstractValidator<HideGenreCommand>
{
    public HideGenreCommandValidator()
    {
        RuleFor(hideGenreCommand => hideGenreCommand.Id).NotEqual(Guid.Empty);
    }
}
