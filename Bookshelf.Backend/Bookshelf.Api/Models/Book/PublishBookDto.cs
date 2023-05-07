using AutoMapper;
using Bookshelf.Application.Books.Commands.PublishBook;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Api.Models.Book;

public class PublishBookDto : IMapWith<PublishBookCommand>
{
    public Guid BookId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<PublishBookDto, PublishBookCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.BookId));
}
