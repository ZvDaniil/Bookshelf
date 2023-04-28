using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.UpdateGenre;

namespace Bookshelf.Api.Models;

public class UpdateGenreDto : IMapWith<UpdateGenreCommand>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool Visible { get; set; }

    public void Maping(Profile profile) =>
        profile.CreateMap<UpdateGenreDto, UpdateGenreCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
            .ForMember(command => command.Visible, opt => opt.MapFrom(dto => dto.Visible));
}