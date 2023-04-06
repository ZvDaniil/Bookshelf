namespace Bookshelf.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
}