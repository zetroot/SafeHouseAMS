// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace SafeHouseAMS.IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
           };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ("appdataapi", "Scope for all application data")
                {
                    Scopes = {"appdata"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new("appdata")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new ()
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = {"https://localhost:5001"},
                    AllowedScopes = {"openid", "profile", "appdata"},
                    RedirectUris = {"https://localhost:5001/authentication/login-callback"},
                    PostLogoutRedirectUris = {"https://localhost:5001"},
                    Enabled = true
                }
            };
    }
}
