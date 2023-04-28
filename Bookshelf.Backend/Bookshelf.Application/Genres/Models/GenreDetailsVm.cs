using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Books.Models;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Genres.Models;

public class GenreDetailsVm : IMapWith<Genre>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<BookLookupDto> Books { get; set; } = new();

    public void Mapping(Profile profile) =>
        profile.CreateMap<Genre, GenreDetailsVm>()
            .ForMember(genreVm => genreVm.Id, opt => opt.MapFrom(genre => genre.Id))
            .ForMember(genreVm => genreVm.Name, opt => opt.MapFrom(genre => genre.Name))
            .ForMember(genreVm => genreVm.Books, opt => opt.MapFrom(genre => genre.Books));
}