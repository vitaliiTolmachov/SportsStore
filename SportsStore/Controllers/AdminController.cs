using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private IProductRepository _repository;
		public AdminController(IProductRepository repo) {
			_repository = repo;
		}
		// GET: Admin
		public ViewResult Index() {
			return View(_repository.Products);
		}


		//// GET: Admin/Create
		public ActionResult Create() {
			return View("Edit", new Product());
		}

		public ViewResult Edit(int id) {
			Product product = _repository.Products.
				FirstOrDefault(p => p.ProductId.Equals(id));
			//ViewData["CategoryList"] = (IEnumerable<SelectListItem>)_repository.CategotyList;
			return View(product);
		}

		//// POST: Admin/Edit/5
		[HttpPost]
		public ActionResult Edit(Product product, HttpPostedFileBase image = null) {
			try {
				if (ModelState.IsValid) {
					if (image != null)
					{
						product.ImageMimeType = image.ContentType;
						product.ImageData = new byte[image.ContentLength];
						image.InputStream.Read(product.ImageData, 0, image.ContentLength);
					}
					_repository.SaveProduct(product);
					TempData["message"] = $"{product.Name} has been saved";
					return RedirectToAction("Index");
				} else {
					//ViewData["CategoryList"] = (IEnumerable<SelectListItem>)_repository.CategotyList;
					return View(product);
				}

			} catch {
				return View(product);
			}
		}

		//// POST: Admin/Delete/5
		[HttpPost]
		public ActionResult Delete(int productId) {
			try {
				// TODO: Add delete logic here
				var product = _repository.Delete(productId);
				if (product != null)
				{
					TempData["message"] = $"{product.Name} was deleted";
				}

				return RedirectToAction("Index");
			} catch {
				return RedirectToAction("Index");
			}
		}
	}
}
