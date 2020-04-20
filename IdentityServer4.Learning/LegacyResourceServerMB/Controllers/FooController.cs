using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LegacyResourceServerMB.Controllers
{
    public class FooController : ApiController
    {
		[HttpGet]
		public string Get()
		{
			return "Foo";
		}
    }
}
