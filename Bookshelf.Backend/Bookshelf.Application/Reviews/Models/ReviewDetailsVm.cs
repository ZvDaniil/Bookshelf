using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Reviews.Models;

public class ReviewDetailsVm : IMapWith<Review>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public int Rating { get; set; }
    public string Content { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public Guid BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Review, ReviewDetailsVm>()
            .ForMember(vm => vm.Id, opt => opt.MapFrom(review => review.Id))
            .ForMember(vm => vm.Rating, opt => opt.MapFrom(review => review.Rating))
            .ForMember(vm => vm.Content, opt => opt.MapFrom(review => review.Content))
            .ForMember(vm => vm.UserName, opt => opt.MapFrom(review => review.UserName))
            .ForMember(vm => vm.BookId, opt => opt.MapFrom(review => review.BookId))
            .ForMember(vm => vm.BookTitle, opt => opt.MapFrom(review => review.Book.Title))
            .ForMember(vm => vm.Visible, opt => opt.MapFrom(review => review.Visible));
}
