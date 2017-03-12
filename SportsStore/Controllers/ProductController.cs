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
					ProductsCount = category == null?
					_repository.Products.Count() :
					_repository.Products.Where(p=>p.Category.Equals(category)).Count()
				},
				Products = _repository.Products
					.Where(p => category == null ||
					p.Category == category)
					.OrderBy(p => p.ProductId)
					.Skip((currentPage - 1) * this.ProductsPerPage)
					.Take(ProductsPerPage),
				Category = category
			};
			return View(model);
		}
		public FileContentResult GetImage(int productId)
		{
			var product = _repository.Products.FirstOrDefault(prod => prod.ProductId.Equals(productId));
			if (product != null)
			{
				return File(product.ImageData, product.ImageMimeType);
			}else{
				return null;
			}
		}
	}
}