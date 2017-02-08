﻿using System;
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

		public NavController(IProductRepository repo)
		{
			this.productRepository = repo;
		}
		// GET: Nav
		public PartialViewResult Menu()
		{
			IEnumerable<string> categories = productRepository.Products
				.Select(product => product.Category)
				.Distinct()
				.OrderBy(category => category);

			return PartialView(categories);
		}
	}
}