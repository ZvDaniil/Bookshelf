namespace Bookshelf.Application.Books.Models;

public class BookListVm
{
    public IList<BookLookupDto> Books { get; set; } = default!;
}