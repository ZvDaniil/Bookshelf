using Bookshelf.Domain.Interfaces;

namespace Bookshelf.Domain;

/// <summary>
/// Модель отзыва на книгу.
/// </summary>
public class Review : IPublished
{
    /// <summary>
    /// Идентификатор отзыва.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Оценка.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Содержание.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор книги.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Книга.
    /// </summary>
    public virtual Book Book { get; set; } = null!;

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    ///<inheritdoc/>
    public bool Visible { get; set; }
}
