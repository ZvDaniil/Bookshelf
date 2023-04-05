using AutoMapper;
using Bookshelf.Application.Books.Commands.UpdateBook;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Api.Models;

public class UpdateBookDto : IMapWith<UpdateBookCommand>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AgeRestriction { get; set; }
    public DateTime DatePublished { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }
    public string ISBN { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<UpdateBookDto, UpdateBookCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Title, opt => opt.MapFrom(dto => dto.Title))
            .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description))
            .ForMember(command => command.AgeRestriction, opt => opt.MapFrom(dto => dto.AgeRestriction))
            .ForMember(command => command.DatePublished, opt => opt.MapFrom(dto => dto.DatePublished))
            .ForMember(command => command.Pages, opt => opt.MapFrom(dto => dto.Pages))
            .ForMember(command => command.Price, opt => opt.MapFrom(dto => dto.Price))
            .ForMember(command => command.ISBN, opt => opt.MapFrom(dto => dto.ISBN));
}