using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.CreateGenre;

namespace Bookshelf.Api.Models.Genre;

public class CreateGenreDto : IMapWith<CreateGenreCommand>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<CreateGenreDto, CreateGenreCommand>()
            .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
            .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description))
            .ForMember(command => command.Visible, opt => opt.MapFrom(dto => dto.Visible));
}
