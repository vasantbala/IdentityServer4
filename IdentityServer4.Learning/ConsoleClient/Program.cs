using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
	public class Program
	{
		async static Task Main(string[] args)
		{
			string quit = string.Empty;

			while (quit != "quit")
			{
				await Foo();
				quit = Console.ReadLine();
			}

			
		}

		async static Task Foo()
		{
			try
			{
				var token = await GetClientToken();

				var apiClient = new HttpClient();
				apiClient.SetBearerToken(token.AccessToken);
				var response = await apiClient.GetAsync("https://localhost:44327/api/bar/get");
				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine(response.StatusCode);
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();
					Console.WriteLine(content);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		async static Task<TokenResponse> GetClientToken()
		{
			var client = new HttpClient();
			var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				throw new Exception("Disco error");
			}

			var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = disco.TokenEndpoint,
				ClientId = "client",
				ClientSecret = "secret",
				Scope = "LegacyResourceAPI"
			});

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				throw new Exception("Token error");
			}
			return tokenResponse;
		}

	}
}
