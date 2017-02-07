using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			////Empty
			//routes.MapRoute(
			//	name: null,
			//	url: "",
			//	defaults: new {
			//		controller = "Product",
			//		action = "List",
			//		caterory = (string)null,
			//		currentPage = 1
			//	});
			//Empty
			routes.MapRoute(null,
				"",
				new {
					controller = "Product",
					action = "List",
					caterory = (string)null,
					currentPage = 1
				});

			//?currentPage=1
			routes.MapRoute(null,
				"Page{currentPage}",
				new {
					controller = "Product",
					action = "List",
					caterory = string.Empty
				},
				new {
					currentPage = @"\d+"
				});

			//?Soccer
			routes.MapRoute(null,
				"{category}",
				new {
					controller = "Product",
					action = "List",
					currentPage = 1
				});
			//?Soccer/Page=1
			routes.MapRoute(null,
				"{category}/Page{currentPage}",
				new {
					controller = "Product",
					action = "List"
				},
				new {
					currentPage = @"\d+"
				});
			routes.MapRoute(null, "{controller}/{action}");
		}
	}
}
