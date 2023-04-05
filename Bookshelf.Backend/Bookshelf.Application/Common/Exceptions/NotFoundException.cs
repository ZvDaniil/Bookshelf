namespace Bookshelf.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, Guid key)
        : base($"Entity \"{name}\" ({key}) not found.")
    {
    }

    public NotFoundException(string name, IEnumerable<Guid> keys)
        : base($"Entities \"{name}\" ({string.Join(",", keys)}) not found.")
    {
    }
}