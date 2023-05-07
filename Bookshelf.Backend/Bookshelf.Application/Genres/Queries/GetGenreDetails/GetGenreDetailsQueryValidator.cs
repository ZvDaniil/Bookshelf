using FluentValidation;

namespace Bookshelf.Application.Genres.Queries.GetGenreDetails;

public class GetGenreDetailsQueryValidator : AbstractValidator<GetGenreDetailsQuery>
{
    public GetGenreDetailsQueryValidator()
    {
        RuleFor(getGenreDetailsQuery => getGenreDetailsQuery.Id).NotEqual(Guid.Empty);
    }
}
