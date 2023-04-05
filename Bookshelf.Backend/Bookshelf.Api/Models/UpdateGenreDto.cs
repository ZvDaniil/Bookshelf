using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.UpdateGenre;

namespace Bookshelf.Api.Models;

public class UpdateGenreDto : IMapWith<UpdateGenreCommand>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Maping(Profile profile) =>
        profile.CreateMap<UpdateGenreDto, UpdateGenreCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name));
}