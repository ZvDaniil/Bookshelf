using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Reviews.Models;

public class ReviewLookupDto : IMapWith<Review>
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public static void Mapping(Profile profile) =>
        profile.CreateMap<Review, ReviewLookupDto>()
            .ForMember(vm => vm.Id, opt => opt.MapFrom(review => review.Id))
            .ForMember(vm => vm.UserId, opt => opt.MapFrom(review => review.UserId))
            .ForMember(vm => vm.Rating, opt => opt.MapFrom(review => review.Rating))
            .ForMember(vm => vm.Content, opt => opt.MapFrom(review => review.Content))
            .ForMember(vm => vm.UserName, opt => opt.MapFrom(review => review.UserName))
            .ForMember(vm => vm.Visible, opt => opt.MapFrom(review => review.Visible));
}