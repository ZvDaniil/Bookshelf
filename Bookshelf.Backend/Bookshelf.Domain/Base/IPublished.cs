namespace Bookshelf.Domain.Interfaces;

/// <summary>
/// Entity can be hide or publish
/// </summary>
public interface IPublished
{
    ///<summary>
    ///Indicate that the entity visible
    ///</summary>
    public bool Visible { get; set; }
}