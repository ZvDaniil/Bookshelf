namespace Bookshelf.Domain;

/// <summary>
/// Модель книги.
/// </summary>
public class Book
{
    /// <summary>
    /// Идентификатор книги.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Название книги.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание книги.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Возрастное ограничение книги.
    /// </summary>
    public int AgeRestriction { get; set; }

    /// <summary>
    /// Дата публикации книги.
    /// </summary>
    public DateTime DatePublished { get; set; }

    /// <summary>
    /// Количество страниц в книге.
    /// </summary>
    public int Pages { get; set; }

    /// <summary>
    /// Цена книги.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Идентификатор ISBN книги.
    /// </summary>
    public string ISBN { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор автора книги.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Автор книги.
    /// </summary>
    public Author Author { get; set; }

    /// <summary>
    /// Коллекция жанров, к которым относится книга.
    /// </summary>
    public ICollection<Genre> Genres { get; set; }

    /// <summary>
    /// Коллекция отзывов на книгу.
    /// </summary>
    public ICollection<Review> Reviews { get; set; }
}
