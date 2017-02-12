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
	public class CartController : Controller
	{
		private IProductRepository _repository;

		public CartController(IProductRepository repo) {
			this._repository = repo;
		}
		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = this.GetCart(),
				ReturnUrl = returnUrl
			});
		}
		private Cart GetCart() {
			Cart cart = (Cart)Session["Cart"];
			if (cart == null) {
				cart = new Cart();
				Session["Cart"] = cart;
			}
			return cart;

		}
		public RedirectToRouteResult AddToCart(int productId, string returnUrl) {
			Product product = this._repository.Products
				.Where(p => p.ProductId.Equals(productId))
				.FirstOrDefault();

			if (product != null)
				this.GetCart().Add(product, 1);
			return RedirectToAction("Index", new {returnUrl});
		}
		public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = this._repository.Products
				.Where(p => p.ProductId.Equals(productId))
				.FirstOrDefault();
			if(product!=null)
				this.GetCart().Remove(product);
			return RedirectToAction("Index", new {returnUrl});
		}
	}
}