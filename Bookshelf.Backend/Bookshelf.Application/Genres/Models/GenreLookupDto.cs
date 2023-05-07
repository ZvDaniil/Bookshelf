using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Genres.Models;

public class GenreLookupDto : IMapWith<Genre>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Genre, GenreLookupDto>()
            .ForMember(genreDto => genreDto.Id, opt => opt.MapFrom(genre => genre.Id))
            .ForMember(genreDto => genreDto.Name, opt => opt.MapFrom(genre => genre.Name))
            .ForMember(genreDto => genreDto.Visible, opt => opt.MapFrom(genre => genre.Visible));
}