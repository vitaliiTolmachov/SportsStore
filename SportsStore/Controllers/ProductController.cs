using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.Controllers
{
	public class ProductController : Controller
	{
		private IProductRepository _repository;

		public int ProductsPerPage = 4;
		public ProductController(IProductRepository paramRepository) {
			this._repository = paramRepository;
		}
		// GET: Product
		public ViewResult List(int currentPage = 1)
		{
			return View(_repository.Products
				.OrderBy(p=>p.ProductId)
				.Skip((currentPage - 1)*this.ProductsPerPage)
				.Take(ProductsPerPage));
		}
	}
}