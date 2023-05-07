using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Books.Models;

public class BookLookupDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public AuthorLookupDto Author { get; set; } = default!;
    public double AverageRating { get; set; }
    public int ReviewsCount { get; set; }
    public bool Visible { get; set; }
}