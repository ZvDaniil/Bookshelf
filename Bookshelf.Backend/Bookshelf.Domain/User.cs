namespace Bookshelf.Domain;

/// <summary>
/// Модель пользователя.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Список отзывов.
    /// </summary>
    public virtual ICollection<Review>? Reviews { get; set; }
}