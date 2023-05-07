using FluentValidation;

namespace Bookshelf.Application.Books.Queries.GetBookDetails;

public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(getBookDetailsQuery => getBookDetailsQuery.Id).NotEqual(Guid.Empty);
    }
}