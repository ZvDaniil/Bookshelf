using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Commands.HideAuthor;

namespace Bookshelf.Api.Models.Author;

public class HideAuthorDto : IMapWith<HideAuthorCommand>
{
    public Guid AuthorId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<HideAuthorDto, HideAuthorCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.AuthorId));
}