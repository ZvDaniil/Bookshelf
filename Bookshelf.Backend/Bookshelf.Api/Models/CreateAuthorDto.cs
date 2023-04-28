using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Commands.CreateAuthor;

namespace Bookshelf.Api.Models;

public class CreateAuthorDto : IMapWith<CreateAuthorCommand>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<CreateAuthorDto, CreateAuthorCommand>()
            .ForMember(command => command.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
            .ForMember(command => command.LastName, opt => opt.MapFrom(dto => dto.LastName))
            .ForMember(command => command.Visible, opt => opt.MapFrom(dto => dto.Visible));
}
