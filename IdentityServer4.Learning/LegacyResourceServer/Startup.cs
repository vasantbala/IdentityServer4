﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using LegacyResourceServer.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LegacyResourceServer.Startup))]

namespace LegacyResourceServer
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

			HttpConfiguration httpConfiguration = new HttpConfiguration();
			WebApiConfig.Register(httpConfiguration);
			app.UseWebApi(httpConfiguration);
		}
	}
}
