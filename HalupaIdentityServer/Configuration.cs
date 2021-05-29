using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalupaIdentityServer
{
    public static class Configuration
    {
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
                ClientSecrets =
                {
                    new Secret("client_secret".ToSha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "HalupaApi" }
            }
        };

        public static IEnumerable<ApiScope> GetScopes() => new List<ApiScope>
        {
            new ApiScope("HalupaApi")
        };
    }
}
