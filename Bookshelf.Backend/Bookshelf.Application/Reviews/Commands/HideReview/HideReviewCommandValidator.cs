using FluentValidation;

namespace Bookshelf.Application.Reviews.Commands.HideReview;

public class HideReviewCommandValidator : AbstractValidator<HideReviewCommand>
{
    public HideReviewCommandValidator()
    {
        RuleFor(hideReviewCommand => hideReviewCommand.Id).NotEqual(Guid.Empty);
    }
}
