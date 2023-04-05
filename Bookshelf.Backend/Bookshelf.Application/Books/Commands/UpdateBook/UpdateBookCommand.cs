using MediatR;

namespace Bookshelf.Application.Books.Commands.UpdateBook;

public class UpdateBookCommand : IRequest
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AgeRestriction { get; set; }

    public DateTime DatePublished { get; set; }

    public int Pages { get; set; }

    public decimal Price { get; set; }

    public string ISBN { get; set; } = string.Empty;
}
