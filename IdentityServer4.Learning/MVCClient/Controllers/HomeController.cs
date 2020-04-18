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

			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var content = await client.GetStringAsync("http://localhost:6000/api/identity");
			ViewBag.Json = JArray.Parse(content).ToString();

			try
			{
				var clientz = new HttpClient();
				clientz.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				var fooContent = await clientz.GetStringAsync("http://localhost:44327/api/foo/get");
				ViewBag.Foo = fooContent;
			}
			catch (Exception ex)
			{
				
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
