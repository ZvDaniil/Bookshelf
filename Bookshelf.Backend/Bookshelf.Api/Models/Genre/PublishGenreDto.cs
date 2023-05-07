using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Genres.Commands.PublishGenre;

namespace Bookshelf.Api.Models.Genre;

public class PublishGenreDto : IMapWith<PublishGenreCommand>
{
    public Guid GenreId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<PublishGenreDto, PublishGenreCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.GenreId));
}
