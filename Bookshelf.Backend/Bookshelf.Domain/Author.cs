using Bookshelf.Domain.Interfaces;

namespace Bookshelf.Domain;

/// <summary>
/// Модель автора книги.
/// </summary>
public class Author : IPublished
{
    /// <summary>
    /// Уникальный идентификатор автора.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Список книг, написанных автором.
    /// </summary>
    public virtual ICollection<Book>? Books { get; set; }

    ///<inheritdoc/>
    public bool Visible { get; set; }
}