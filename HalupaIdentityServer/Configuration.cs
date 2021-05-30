using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            new ApiResource("HalupaApi")
            {
                Scopes =
                {
                    "HalupaApi"
                }
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
                    "HalupaApi",
                    IdentityServerConstants.StandardScopes.OpenId
                },
                RequireConsent = false,
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowAccessTokensViaBrowser = true
            }
        };

        public static IEnumerable<ApiScope> GetScopes() => new List<ApiScope>
        {
            new ApiScope("HalupaApi")
        };
    }
}
