using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Books.Commands.CreateBook;

namespace Bookshelf.Api.Models;

public class CreateBookDto : IMapWith<CreateBookCommand>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AgeRestriction { get; set; }
    public DateTime DatePublished { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public List<Guid> GenreIds { get; set; } = new();

    public void Mapping(Profile profile) =>
        profile.CreateMap<CreateBookDto, CreateBookCommand>()
            .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
            .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description))
            .ForMember(command => command.AgeRestriction, opt => opt.MapFrom(dto => dto.AgeRestriction))
            .ForMember(command => command.DatePublished, opt => opt.MapFrom(dto => dto.DatePublished))
            .ForMember(command => command.Pages, opt => opt.MapFrom(dto => dto.Pages))
            .ForMember(command => command.ISBN, opt => opt.MapFrom(dto => dto.ISBN))
            .ForMember(command => command.AuthorId, opt => opt.MapFrom(dto => dto.AuthorId))
            .ForMember(command => command.GenreIds, opt => opt.MapFrom(dto => dto.GenreIds));
}