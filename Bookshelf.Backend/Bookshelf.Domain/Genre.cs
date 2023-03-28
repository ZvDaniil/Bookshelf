namespace Bookshelf.Domain;

/// <summary>
/// Модель жанра книги.
/// </summary>
public class Genre
{
    /// <summary>
    /// Идентификатор жанра.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название жанра.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список книг данного жанра.
    /// </summary>
    public ICollection<Book> Books { get; set; }
}