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
		public ViewResult Index(Cart cart,string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}
		public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl) {
			Product product = this._repository.Products
				.Where(p => p.ProductId.Equals(productId))
				.FirstOrDefault();

			if (product != null)
                cart.Add(product, 1);
			return RedirectToAction("Index", new {returnUrl});
		}
		public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl)
		{
			Product product = this._repository.Products
				.Where(p => p.ProductId.Equals(productId))
				.FirstOrDefault();
			if(product!=null)
                cart.Remove(product);
			return RedirectToAction("Index", new {returnUrl});
		}
	}
}