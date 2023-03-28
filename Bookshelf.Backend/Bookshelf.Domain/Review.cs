namespace Bookshelf.Domain;

/// <summary>
/// Модель отзыва на книгу
/// </summary>
public class Review
{
    /// <summary>
    /// Идентификатор отзыва
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Заголовок отзыва
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Текст отзыва
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Рейтинг отзыва (от 1 до 5)
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Дата создания отзыва
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Идентификатор книги, на которую написан отзыв
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Книга, на которую написан отзыв
    /// </summary>
    public Book Book { get; set; }
}

