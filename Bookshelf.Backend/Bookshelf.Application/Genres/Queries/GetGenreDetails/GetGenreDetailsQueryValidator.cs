using FluentValidation;

namespace Bookshelf.Application.Genres.Queries.GetGenreDetails;

internal class GetGenreDetailsQueryValidator : AbstractValidator<GetGenreDetailsQuery>
{
    public GetGenreDetailsQueryValidator()
    {
        RuleFor(getGenreDetailsQuery => getGenreDetailsQuery.Id).NotEqual(Guid.Empty);
    }
}
