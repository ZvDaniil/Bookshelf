using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Genres.Models;

public class GenreLookupDto : IMapWith<Genre>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<Genre, GenreLookupDto>()
            .ForMember(genreDto => genreDto.Id, opt => opt.MapFrom(genre => genre.Id))
            .ForMember(genreDto => genreDto.Name, opt => opt.MapFrom(genre => genre.Name));
}