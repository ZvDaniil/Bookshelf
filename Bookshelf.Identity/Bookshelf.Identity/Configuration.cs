using IdentityModel;
using IdentityServer4.Models;

namespace Bookshelf.Identity;

public static class Configuration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("BookshelfWebAPI", "Web API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "User role(s)", new List<string> { "role" })
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("BookshelfWebAPI", "Web API", new [] { JwtClaimTypes.Name })
            {
                Scopes = {"BookshelfWebAPI"},
                UserClaims = { "role" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "api-swagger",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RedirectUris = { "https://localhost:7093/oauth2-redirect.html" },
                AllowedCorsOrigins = { "https://localhost:7093" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "BookshelfWebAPI", "roles" }
            },
        };
}