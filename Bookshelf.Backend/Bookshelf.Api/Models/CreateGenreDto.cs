using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.CreateGenre;

namespace Bookshelf.Api.Models;

public class CreateGenreDto : IMapWith<CreateGenreCommand>
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<CreateGenreDto, CreateGenreCommand>()
            .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name));
}
