using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Authors.Commands.UpdateAuthor;

namespace Bookshelf.Api.Models;

public class UpdateAuthorDto : IMapWith<UpdateAuthorCommand>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<UpdateAuthorDto, UpdateAuthorCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.FirstName, opt => opt.MapFrom(dto => dto.FirstName))
            .ForMember(command => command.LastName, opt => opt.MapFrom(dto => dto.LastName))
            .ForMember(command => command.Visible, opt => opt.MapFrom(dto => dto.Visible));
}