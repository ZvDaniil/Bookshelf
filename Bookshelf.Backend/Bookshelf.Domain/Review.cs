namespace Bookshelf.Domain;

/// <summary>
/// Модель отзыва на книгу.
/// </summary>
public class Review
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
    /// Комментарий.
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор книги.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Книга.
    /// </summary>
    public Book Book { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public User User { get; set; }
}
