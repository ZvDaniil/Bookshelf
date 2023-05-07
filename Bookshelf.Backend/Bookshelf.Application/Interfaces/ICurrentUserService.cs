namespace Bookshelf.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string UserName { get; }
    bool CurrentUserIsInRole(string role);
}