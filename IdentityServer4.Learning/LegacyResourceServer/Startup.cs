using System;
using System.Threading.Tasks;
using System.Web.Http;
using LegacyResourceServer.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(LegacyResourceServer.Startup))]

namespace LegacyResourceServer
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

			app.UseCookieAuthentication(new CookieAuthenticationOptions());
			app.UseIdentityServerBearerTokenAuthentication(new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions 
			{
				Authority = "http://localhost:5000",
				RequiredScopes = new[] { "LegacyResourceAPI" }

			});

			HttpConfiguration httpConfiguration = new HttpConfiguration();
			httpConfiguration.MapHttpAttributeRoutes();
			WebApiConfig.Register(httpConfiguration);
			httpConfiguration.Filters.Add(new AuthorizeAttribute());
			app.UseWebApi(httpConfiguration);
		}
	}
}
