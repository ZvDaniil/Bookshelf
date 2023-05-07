using System.Security.Claims;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext? HttpContext => _httpContextAccessor.HttpContext;
    private ClaimsPrincipal? User => HttpContext?.User;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public Guid UserId
    {
        get
        {
            var id = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        }
    }

    public string UserName => User?.Identity?.Name ?? string.Empty;

    public bool CurrentUserIsInRole(string role) =>
        User is not null && User.IsInRole(role);
}