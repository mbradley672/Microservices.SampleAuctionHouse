﻿using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("auctionApp", "Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client() { // Lack of security due to development
                ClientId = "postman",
                ClientName = "postman",
                AllowedScopes = {"openid", "profile","auctionApp"},
                RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = new[] { new Secret("NotASecret".Sha512())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client() {
                ClientId = "nextFrontend",
                ClientName = "nextFrontend",
                ClientSecrets = { new Secret("secret".Sha512())},
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = true,
                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile","auctionApp"},
                AccessTokenLifetime = 3600*24
            }
        };
}
