using FluentValidation;

namespace Bookshelf.Application.Reviews.Queries.GetReviewDetails;

public class GetReviewDetailsQueryValidator : AbstractValidator<GetReviewDetailsQuery>
{
    public GetReviewDetailsQueryValidator()
    {
        RuleFor(getReviewDetailsQuery => getReviewDetailsQuery.Id).NotEqual(Guid.Empty);
    }
}