using FluentValidation;

namespace Bookshelf.Application.Genres.Commands.PublishGenre;

public class PublishGenreCommandValidator : AbstractValidator<PublishGenreCommand>
{
    public PublishGenreCommandValidator()
    {
        RuleFor(publishGenreCommand => publishGenreCommand.Id).NotEqual(Guid.Empty);
    }
}
