using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Commands.PublishAuthor;

namespace Bookshelf.Api.Models.Author;

public class PublishAuthorDto : IMapWith<PublishAuthorCommand>
{
    public Guid Id { get; set; }
    public bool PublishBooks { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<PublishAuthorDto, PublishAuthorCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.PublishBooks, opt => opt.MapFrom(dto => dto.PublishBooks));
}