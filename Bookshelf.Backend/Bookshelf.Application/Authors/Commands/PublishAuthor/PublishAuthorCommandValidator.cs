using FluentValidation;

namespace Bookshelf.Application.Authors.Commands.PublishAuthor;

public class PublishAuthorCommandValidator : AbstractValidator<PublishAuthorCommand>
{
    public PublishAuthorCommandValidator()
    {
        RuleFor(publishAuthorCommand => publishAuthorCommand.Id).NotEqual(Guid.Empty);
    }
}