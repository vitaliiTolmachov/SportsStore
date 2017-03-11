﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Controllers
{
	public class AdminController : Controller
	{
		private IProductRepository _repository;
		public AdminController(IProductRepository repo)
		{
			_repository = repo;
		}
		// GET: Admin
		public ActionResult Index()
		{
			return View(_repository.Products);
		}

		//// GET: Admin/Details/5
		//public ActionResult Details(int id)
		//{
		//    return View();
		//}

		//// GET: Admin/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		//// POST: Admin/Create
		//[HttpPost]
		//public ActionResult Create(FormCollection collection)
		//{
		//    try
		//    {
		//        // TODO: Add insert logic here

		//        return RedirectToAction("Index");
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: Admin/Edit/5
		public ViewResult Edit(int id)
		{
			Product product = _repository.Products.
				FirstOrDefault(p => p.ProductId.Equals(id));
			return View(product);
		}

		//// POST: Admin/Edit/5
		//[HttpPost]
		//public ActionResult Edit(int id, FormCollection collection)
		//{
		//    try
		//    {
		//        // TODO: Add update logic here

		//        return RedirectToAction("Index");
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: Admin/Delete/5
		//public ActionResult Delete(int id)
		//{
		//    return View();
		//}

		//// POST: Admin/Delete/5
		//[HttpPost]
		//public ActionResult Delete(int id, FormCollection collection)
		//{
		//    try
		//    {
		//        // TODO: Add delete logic here

		//        return RedirectToAction("Index");
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}
	}
}
