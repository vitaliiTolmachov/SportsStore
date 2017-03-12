using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Infrastructure.Abstract;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	public class AccountController : Controller
	{
		private IAuthProvider _authProvider;
		public AccountController(IAuthProvider authProvider)
		{
			_authProvider = authProvider;
		}
		// GET: Account
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl) {
			if (ModelState.IsValid)
			{
				var res = _authProvider.Authentificate(model.UserName, model.Password);
				if (res)
				{
					return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
				}
				else
				{
					ModelState.AddModelError(string.Empty,"Wrong username or password");
					return View();
				}
			}
			else
			{
				return View();
			}
		}
	}
}