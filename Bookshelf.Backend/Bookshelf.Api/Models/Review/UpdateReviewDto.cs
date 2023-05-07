using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Reviews.Commands.UpdateReview;

namespace Bookshelf.Api.Models.Review;

public class UpdateReviewDto : IMapWith<UpdateReviewCommand>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<UpdateReviewDto, UpdateReviewCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Rating, opt => opt.MapFrom(dto => dto.Rating))
            .ForMember(command => command.Content, opt => opt.MapFrom(dto => dto.Content));
}
