namespace Bookshelf.Application.Reviews.Models;

public class ReviewListVm
{
    public IList<ReviewLookupDto> Reviews { get; set; } = default!;
}