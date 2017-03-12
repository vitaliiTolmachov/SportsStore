using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SportsStore.Infrastructure.Abstract;

namespace SportsStore.Infrastructure.Concrete
{
	public class AuthProvider : IAuthProvider
	{
		public bool Authentificate(string username, string password)
		{
			bool result = FormsAuthentication.Authenticate(username, password);
			if (result) {
				FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);
			}
			return result;
		}
	}
}