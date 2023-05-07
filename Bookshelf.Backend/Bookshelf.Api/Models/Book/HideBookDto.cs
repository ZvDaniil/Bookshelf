using AutoMapper;
using Bookshelf.Application.Books.Commands.HideBook;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Api.Models.Book;

public class HideBookDto : IMapWith<HideBookCommand>
{
    public Guid BookId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<HideBookDto, HideBookCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.BookId));
}
