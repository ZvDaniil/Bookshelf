using Bookshelf.Domain.Interfaces;

namespace Bookshelf.Domain;

/// <summary>
/// Модель жанра книги.
/// </summary>
public class Genre : IPublished
{
    /// <summary>
    /// Идентификатор жанра.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название жанра.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Список книг данного жанра.
    /// </summary>
    public virtual ICollection<Book>? Books { get; set; }

    ///<inheritdoc/>
    public bool Visible { get; set; }
}