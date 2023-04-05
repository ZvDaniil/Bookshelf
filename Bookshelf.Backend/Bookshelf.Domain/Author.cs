namespace Bookshelf.Domain;

/// <summary>
/// Модель автора книги.
/// </summary>
public class Author
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
    public ICollection<Book> Books { get; set; }
}