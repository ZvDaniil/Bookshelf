using AutoMapper;
using Bookshelf.Domain;
using Bookshelf.Application.Common.Mappings;

namespace Bookshelf.Application.Authors.Models;

public class AuthorLookupDto : IMapWith<Author>
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public void Mapping(Profile profile) =>
        profile.CreateMap<Author, AuthorLookupDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(author => author.Id))
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(author => $"{author.FirstName} {author.LastName}"));
}