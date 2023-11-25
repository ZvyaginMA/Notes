using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Notes.Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("NotesApi", "Web Api")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("NotesApi", "Web Api", new[]
                { JwtClaimTypes.Name})
                {
                    Scopes = { "NotesApi" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
               new Client()
               {
                   ClientId = "notes-web-api",
                   ClientName = "Notes Web",
                   AllowedGrantTypes = GrantTypes.Code,
                   RequireClientSecret = false,
                   RedirectUris =
                   {
                       "http://...//signin-oidc"
                   },
                   AllowedCorsOrigins =
                   {
                       "http://..."
                   },
                   PostLogoutRedirectUris =
                   {
                       "http://...//signin-oidc"
                   },
                   AllowedScopes =
                   {
                       IdentityServerConstants.StandardScopes.OpenId, 
                       IdentityServerConstants.StandardScopes.Profile,
                       "NotesApi"
                   },
                   AllowAccessTokensViaBrowser = true,
               }
            };
    }
}
