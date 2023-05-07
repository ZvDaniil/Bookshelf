namespace Bookshelf.Application.Authors.Models;

public class AuthorListVm
{
    public IList<AuthorLookupDto> Authors { get; set; } = default!;
}