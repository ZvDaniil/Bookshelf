using FluentValidation;

namespace Bookshelf.Application.Reviews.Commands.PublishReview;

public class PublishReviewCommandValidator : AbstractValidator<PublishReviewCommand>
{
    public PublishReviewCommandValidator()
    {
        RuleFor(publishReviewCommand => publishReviewCommand.Id).NotEqual(Guid.Empty);
    }
}