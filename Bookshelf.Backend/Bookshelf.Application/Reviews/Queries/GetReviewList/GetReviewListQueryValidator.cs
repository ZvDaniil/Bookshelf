using FluentValidation;

namespace Bookshelf.Application.Reviews.Queries.GetReviewList;

public class GetReviewListQueryValidator : AbstractValidator<GetReviewListQuery>
{
    public GetReviewListQueryValidator()
    {
        RuleFor(getReviewListQuery => getReviewListQuery.BookId).NotEqual(Guid.Empty);
    }
}