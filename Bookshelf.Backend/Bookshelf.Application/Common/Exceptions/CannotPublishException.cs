namespace Bookshelf.Application.Common.Exceptions;

public class CannotPublishException : Exception
{
    public CannotPublishException(string name, Guid key, string message)
        : base($"Cannot publish book \"{name}\" with ID {key}: {message}")
    {
    }
}