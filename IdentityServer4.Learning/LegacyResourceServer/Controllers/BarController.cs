﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LegacyResourceServer.Controllers
{
	[Authorize]
    public class BarController : ApiController
    {
		[HttpGet]
		public string Get()
		{
			return "Bar";
		}
    }
}
