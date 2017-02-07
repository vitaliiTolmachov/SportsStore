using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Models;

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
		public ViewResult List(string category, int currentPage = 1) {
			var model = new ProducListViewModel {
				PagingInfo = new PagingInfo {
					CurrentPage = currentPage,
					ProductsPerPage = this.ProductsPerPage,
					ProductsCount = this._repository.Products.Count()
				},
				Products = _repository.Products
					.Where(p => p.Category == null || p.Category.Equals(category))
					.OrderBy(p => p.ProductId)
					.Skip((currentPage - 1) * this.ProductsPerPage)
					.Take(ProductsPerPage),
				Category = category
			};
			return View(model);
		}
	}
}