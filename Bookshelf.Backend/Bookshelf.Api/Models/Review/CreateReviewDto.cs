using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Reviews.Commands.CreateReview;

namespace Bookshelf.Api.Models.Review;

public class CreateReviewDto : IMapWith<CreateReviewCommand>
{
    public Guid BookId { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<CreateReviewDto, CreateReviewCommand>()
            .ForMember(command => command.BookId, opt => opt.MapFrom(dto => dto.BookId))
            .ForMember(command => command.Rating, opt => opt.MapFrom(dto => dto.Rating))
            .ForMember(command => command.Content, opt => opt.MapFrom(dto => dto.Content));
}
