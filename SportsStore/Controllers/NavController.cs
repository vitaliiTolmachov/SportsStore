using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.Controllers
{
	public class NavController : Controller
	{
		private IProductRepository productRepository;

		public NavController(IProductRepository repo) {
			productRepository = repo;
		}
		// GET: Nav
		public PartialViewResult Menu(string category = null, bool horizontalLayout = false) {
			ViewBag.SelectedCategory = category;

			IEnumerable<string> categories = productRepository.Products
				.Select(product => product.Category)
				.Distinct()
				.OrderBy(x => x);

			//string viewname = horizontalLayout ? "MenuHorizontal" : "Menu";

			return PartialView("FlexMenu",categories);
		}
	}
}