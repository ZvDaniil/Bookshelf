using FluentValidation;

namespace Bookshelf.Application.Books.Queries.GetBookDetails;

internal class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(getBookDetailsQuery => getBookDetailsQuery.Id).NotEqual(Guid.Empty);
    }
}