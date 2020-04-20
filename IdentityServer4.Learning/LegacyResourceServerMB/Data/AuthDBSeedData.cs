using LegacyResourceServerMB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegacyResourceServerMB.Data
{
	public class AuthDBSeedData
	{
		public static void Initialize()
		{
			using (AuthContext ctx = new AuthContext())
			using (UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx)))
			{
				IdentityUser user = new IdentityUser
				{
					UserName = "Administrator"
				};
				var result = userManager.Create(user, "SuperPassword");
			}
		}
	}
}