using System.Security.Claims;

namespace Bookshelf.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    bool CurrentUserIsInRole(string role);
}