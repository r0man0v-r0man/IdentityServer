using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace HalupaIdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                
            };
        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("halupaApi")
            {
                Scopes = { "halupaApi" }
            }
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "HalupaClientId",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "halupaApi"
                },
                RequireConsent = false,
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowAccessTokensViaBrowser = true
            }
        };

        public static IEnumerable<ApiScope> GetScopes() => new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.StandardScopes.OpenId),
            new ApiScope(IdentityServerConstants.StandardScopes.Profile),
            new ApiScope("halupaApi")
        };
    }
}
