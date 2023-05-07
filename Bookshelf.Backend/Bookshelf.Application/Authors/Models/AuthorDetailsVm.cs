using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Books.Models;

namespace Bookshelf.Application.Authors.Models;

public class AuthorDetailsVm : IMapWith<Author>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<BookLookupDto> Books { get; set; } = new();
    public bool Visible { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<Author, AuthorDetailsVm>()
            .ForMember(authorVm => authorVm.Id, opt => opt.MapFrom(author => author.Id))
            .ForMember(authorVm => authorVm.FirstName, opt => opt.MapFrom(author => author.FirstName))
            .ForMember(authorVm => authorVm.Books, opt => opt.MapFrom(author => author.Books))
            .ForMember(authorVm => authorVm.Visible, opt => opt.MapFrom(author => author.Visible));
}