// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Marvin.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Your role(s)",
                    UserClaims = new List<string>
                    {
                        "role"
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScope =>
        new ApiScope[]
        {
                new ApiScope("igread","Image Gallery API Read Image")
        };

        public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("imagegalleryclientapi","Image Gallery API")
            {
                Scopes = { "igread" },
                UserClaims = { "role"}

            }
        };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client{
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44389/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44389/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "igread"
                    },
                    ClientSecrets = new List<Secret>{
                        new Secret("secret".Sha256())
                    }
                }
            };
    }
}