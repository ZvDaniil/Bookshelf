using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Reviews.Commands.HideReview;

namespace Bookshelf.Api.Models.Review;

public class HideReviewDto : IMapWith<HideReviewCommand>
{
    public Guid Id { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<HideReviewDto, HideReviewCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id));
}
