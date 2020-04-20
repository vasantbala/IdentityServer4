using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

				var content = await client.GetStringAsync("http://localhost:6000/api/identity");
				ViewBag.Json = JArray.Parse(content).ToString();
			}

			using (var clientz = new HttpClient())
			{
				clientz.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				var fooContent = await clientz.GetStringAsync("https://localhost:44327/api/foo/get");
				ViewBag.Foo = fooContent;

				var barContent = await clientz.GetStringAsync("https://localhost:44327/api/bar/get");
				ViewBag.Bar = barContent;
			}

			using (var client = new HttpClient())
			{
				string oauthTokenUrl = "https://localhost:44328/token";
				var cred = new List<KeyValuePair<string, string>>();
				cred.Add(new KeyValuePair<string, string>("grant_type", "password"));
				cred.Add(new KeyValuePair<string, string>("username", "Administrator"));
				cred.Add(new KeyValuePair<string, string>("password", "SuperPassword"));

				var req = new HttpRequestMessage(HttpMethod.Post, oauthTokenUrl) { Content = new FormUrlEncodedContent(cred) };
				var oauthTokenResult = await client.SendAsync(req);
				if (oauthTokenResult.IsSuccessStatusCode)
				{
					string oauthContent = await oauthTokenResult.Content.ReadAsStringAsync();
					string oauthAccessToken = (string)JObject.Parse(oauthContent).SelectToken("access_token");
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthAccessToken);
					var fooContent = await client.GetStringAsync("https://localhost:44328/api/foo");
					ViewBag.OAuthFoo = fooContent;
				}
			}
				

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

	}
}
