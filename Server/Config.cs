using IdentityServer4.Models;

namespace Server
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string>{"role"}
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope(name:"CoffeeAPI.read", displayName:"Reads my api"),
            new ApiScope(name:"CoffeeAPI.write", displayName:"Writes my api")
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource()
            {
                Name="CoffeeApi",
                Scopes = new List<string> { "CoffeeAPI.read", "CoffeeAPI.write" },
                ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                UserClaims = new List<string> { "role"}
            }
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Cresntials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                AllowedScopes = { "CoffeeAPI.read", "CoffeeAPI.write" }
            },
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                AllowedGrantTypes= GrantTypes.Code,
                RedirectUris = {"https://localhost:7001/signin-oidc" }, /*client:https://localhost:7001*/
                FrontChannelLogoutUri = "https://localhost:7001/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7001/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = {"openid","profile", "CoffeeAPI.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false,
            }
        };
    }
}
