namespace Bookshelf.Domain;

public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция оставленных отзывов.
    /// </summary>
    public ICollection<Review> Reviews { get; set; }
}