// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("resourceAPI", "API Application")
			};
		}


		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",
					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.ClientCredentials,
					// scopes that client has access to
					AllowedScopes = { "api1" }
				},
				new Client
				{
					ClientId = "mvc",
					ClientName = "MVC Client",
					AllowedGrantTypes = GrantTypes.Code,
					ClientSecrets = { new Secret("secret".Sha256()) },
					RedirectUris = { "http://localhost:7000/signin-oidc" },
					PostLogoutRedirectUris = { "http://localhost:7000/signout-callback-oidc" },
					AllowedScopes = {
										IdentityServerConstants.StandardScopes.OpenId,
										IdentityServerConstants.StandardScopes.Profile,
										"resourceAPI"
									}
				}



				//new Client
				//{
				//	ClientId = "clientApp",
				//// no interactive user, use the clientid/secret for authentication
				//	AllowedGrantTypes = GrantTypes.ClientCredentials,
				//// secret for authentication
				//	ClientSecrets =
				//	{
				//		new Secret("secret".Sha256())
				//	},
				//	AllowedScopes = { "resourceAPI" }
				//}
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

	//	public static IEnumerable<IdentityResource> Ids =>
 //           new IdentityResource[]
 //           { 
 //               new IdentityResources.OpenId()
 //           };

 //       public static IEnumerable<ApiResource> Apis =>
	//		new List<ApiResource>
	//	{
	//		new ApiResource("api1", "My API")
	//	};

	//	public static IEnumerable<Client> Clients =>
	//new List<Client>
	//{
	//	new Client
	//	{
	//		ClientId = "client",

 //           // no interactive user, use the clientid/secret for authentication
 //           AllowedGrantTypes = GrantTypes.ClientCredentials,

 //           // secret for authentication
 //           ClientSecrets =
	//		{
	//			new Secret("secret".Sha256())
	//		},

 //           // scopes that client has access to
 //           AllowedScopes = { "api1" }
	//	}
	//};

	}
}