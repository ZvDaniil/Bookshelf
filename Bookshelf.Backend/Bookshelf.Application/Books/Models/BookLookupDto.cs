using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Models;
using Bookshelf.Application.Genres.Models;

namespace Bookshelf.Application.Books.Models;

public class BookLookupDto : IMapWith<Book>
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public AuthorLookupDto Author { get; set; } = default!;

    public decimal Price { get; set; }

    public double AverageRating { get; set; }

    public List<GenreLookupDto> Genres { get; set; } = new();

    public int ReviewsCount { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Book, BookLookupDto>()
            .ForMember(bookDto => bookDto.Id, opt => opt.MapFrom(book => book.Id))
            .ForMember(bookDto => bookDto.Title, opt => opt.MapFrom(book => book.Title))
            .ForMember(bookDto => bookDto.Author, opt => opt.MapFrom(book => book.Author))
            .ForMember(bookDto => bookDto.Price, opt => opt.MapFrom(book => book.Price))
            .ForMember(bookDto => bookDto.Genres, opt => opt.MapFrom(book => book.Genres))
            .ForMember(bookDto => bookDto.ReviewsCount, opt => opt.MapFrom(book => book.Reviews.Count))

            .ForMember(bookdto => bookdto.AverageRating,
                opt => opt.MapFrom(book => book.Reviews.Any()
                    ? book.Reviews.Average(review => review.Rating)
                    : default));
}