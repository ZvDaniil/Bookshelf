using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Genres.Models;

namespace Bookshelf.Application.Books.Models;

public class BookDetailsVm : IMapWith<Book>
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AgeRestriction { get; set; }

    public DateTime DatePublished { get; set; }

    public int Pages { get; set; }

    public decimal Price { get; set; }

    public string ISBN { get; set; } = string.Empty;

    public AuthorLookupDto Author { get; set; } = default!;

    public List<GenreLookupDto> Genres { get; set; } = new();

    public double AverageRating { get; set; }

    public int ReviewsCount { get; set; }

    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Book, BookDetailsVm>()
            .ForMember(bookVm => bookVm.Id, opt => opt.MapFrom(book => book.Id))
            .ForMember(bookVm => bookVm.Title, opt => opt.MapFrom(book => book.Title))
            .ForMember(bookVm => bookVm.Description, opt => opt.MapFrom(book => book.Description))
            .ForMember(bookVm => bookVm.AgeRestriction, opt => opt.MapFrom(book => book.AgeRestriction))
            .ForMember(bookVm => bookVm.DatePublished, opt => opt.MapFrom(book => book.DatePublished))
            .ForMember(bookVm => bookVm.Pages, opt => opt.MapFrom(book => book.Pages))
            .ForMember(bookVm => bookVm.Price, opt => opt.MapFrom(book => book.Price))
            .ForMember(bookVm => bookVm.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(bookVm => bookVm.Author, opt => opt.MapFrom(book => book.Author))
            .ForMember(bookVm => bookVm.Genres, opt => opt.MapFrom(book => book.Genres))
            .ForMember(bookVm => bookVm.Visible, opt => opt.MapFrom(book => book.Visible))

            .ForMember(bookVm => bookVm.ReviewsCount,
                opt => opt.MapFrom(book => book.Reviews != null
                ? book.Reviews.Count
                : 0))

            .ForMember(bookVm => bookVm.AverageRating,
                opt => opt.MapFrom(book => book.Reviews != null && book.Reviews.Any()
                ? book.Reviews.Average(review => review.Rating)
                : 0));
}