using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.HideGenre;

namespace Bookshelf.Api.Models.Genre;

public class HideGenreDto : IMapWith<HideGenreCommand>
{
    public Guid GenreId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<HideGenreDto, HideGenreCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.GenreId));
}
