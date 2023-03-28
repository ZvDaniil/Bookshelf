namespace Bookshelf.Domain;

/// <summary>
/// Модель книги
/// </summary>
public class Book
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание книги
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// URL изображения книги
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Автор книги
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Издательство книги
    /// </summary>
    public string Publisher { get; set; }

    /// <summary>
    /// Дата выхода книги
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Цена книги
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Список жанров, к которым относится книга
    /// </summary>
    public ICollection<Genre> Genres { get; set; }

    /// <summary>
    /// Список отзывов на книгу
    /// </summary>
    public ICollection<Review> Reviews { get; set; }
}

