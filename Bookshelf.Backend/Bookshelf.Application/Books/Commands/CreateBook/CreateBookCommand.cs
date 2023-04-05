using MediatR;

namespace Bookshelf.Application.Books.Commands.CreateBook;

public class CreateBookCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AgeRestriction { get; set; }

    public DateTime DatePublished { get; set; }

    public int Pages { get; set; }

    public decimal Price { get; set; }

    public string ISBN { get; set; } = string.Empty;

    public Guid AuthorId { get; set; }

    public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
}