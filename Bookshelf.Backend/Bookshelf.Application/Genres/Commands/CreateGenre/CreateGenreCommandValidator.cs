using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(createGenreCommand => createGenreCommand.Name).Length(5, 50);
        RuleFor(createGenreCommand => createGenreCommand.Description).MaximumLength(1024);
    }
}