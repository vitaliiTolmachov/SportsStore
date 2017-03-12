using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Infrastructure.Abstract
{
	public interface IAuthProvider
	{
		bool Authentificate(string username, string password);
	}
}