using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.CreateGenre;

internal class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(createGenreCommand => createGenreCommand.Name)
            .MinimumLength(4)
            .MaximumLength(50);
    }
}
