using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Models;

namespace Bookshelf.Application.Books.Models;

public class BookLookupDto : IMapWith<Book>
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public AuthorLookupDto Author { get; set; } = default!;

    public double AverageRating { get; set; }

    public int ReviewsCount { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Book, BookLookupDto>()
            .ForMember(bookDto => bookDto.Id, opt => opt.MapFrom(book => book.Id))
            .ForMember(bookDto => bookDto.Title, opt => opt.MapFrom(book => book.Title))
            .ForMember(bookDto => bookDto.Author, opt => opt.MapFrom(book => book.Author))

            .ForMember(bookDto => bookDto.ReviewsCount,
                opt => opt.MapFrom(book => book.Reviews != null
                    ? book.Reviews.Count
                    : 0))

            .ForMember(bookdto => bookdto.AverageRating,
                opt => opt.MapFrom(book => book.Reviews != null && book.Reviews.Any()
                    ? book.Reviews.Average(review => review.Rating)
                    : 0));
}