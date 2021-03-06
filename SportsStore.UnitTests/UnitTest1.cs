﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.HTMLHelpers;
using SportsStore.Models;

namespace SportsStore.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		Mock<IProductRepository> mock = new Mock<IProductRepository>();

		[TestMethod]
		public void CanPaginate() {
			//Arrange
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1,Name = "Product1"},
				new Product {ProductId = 2,Name = "Product2"},
				new Product {ProductId = 3,Name = "Product3"},
				new Product {ProductId = 4,Name = "Product4"},
				new Product {ProductId =5, Name = "Product5"}
			});
			//Act
			var controller = new ProductController(mock.Object) { ProductsPerPage = 3 };
			var result = (ProducListViewModel)controller.List(category: null, currentPage: 2).Model;

			//Assert
			var productsFromPage2 = result.Products.ToArray();
			Assert.AreEqual(2, productsFromPage2.Length);
			Assert.IsTrue(productsFromPage2[0].ProductId == 4);
			Assert.IsTrue(productsFromPage2[1].ProductId == 5);
		}


		public void CanCreatePaginateLinks() {
			//Arrange
			System.Web.Mvc.HtmlHelper myHelper = null;
			var pageInfo = new PagingInfo {
				ProductsCount = 25,
				ProductsPerPage = 5,
				CurrentPage = 3
			};
			Func<int, string> pageUrlFunc = i => string.Format("currentPage={0} ", i);

			//Act
			var res = myHelper.PageLinks(pageInfo, pageUrlFunc);

			//Assert
			Assert.AreEqual(@"<a class=""btn btn-default"" href=""currentPage=1"">Page 1 </a>"
						+ @"<a class=""btn btn-default"" href=""currentPage=2"">Page 2 </a>"
						+ @"<a class=""btn btn-default btn-primary selected"" href=""currentPage=3"">Page 3 </a>"
						+ @"<a class=""btn btn-default"" href=""currentPage=4"">Page 4 </a>", res.ToString());
		}

		[TestMethod]
		public void Can_Send_Paginate_View_Model() {
			//Arrange
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1,Name = "Product1"},
				new Product {ProductId = 2,Name = "Product2"},
				new Product {ProductId = 3,Name = "Product3"},
				new Product {ProductId = 4,Name = "Product4"},
				new Product {ProductId =5, Name = "Product5"}
			});

			//Act
			var controller = new ProductController(mock.Object) { ProductsPerPage = 3 };
			var result = (ProducListViewModel)controller.List(category: null, currentPage: 2).Model;

			//Assert
			PagingInfo page = result.PagingInfo;
			Assert.AreEqual(page.CurrentPage, 2);
			Assert.AreEqual(page.ProductsPerPage, 3);
			Assert.AreEqual(page.TotalPages, 2);
		}

		//[TestMethod]
		//public void CanCreateCategories() {
		//	//Arrange
		//	var mock = new Mock<IProductRepository>();
		//	var fruits = "Fruits";
		//	var vegetables = "Vegetables";
		//	mock.Setup(m => m.Products).Returns(new[]
		//	{
		//		new Product {ProductId = 1, Name = "Banana", Category = fruits},
		//		new Product {ProductId = 2, Name = "Apple", Category = fruits},
		//		new Product {ProductId = 3, Name = "Potatos", Category = vegetables},
		//		new Product {ProductId = 4, Name = "Orange", Category = fruits},
		//		new Product {ProductId = 5, Name = "Tomato", Category = vegetables}
		//	});
		//	//Act
		//	var target = ((IEnumerable<string>)new NavController(mock.Object).Menu().Model).ToArray();

		//	//Assert
		//	Assert.AreEqual(target.Length, 2);
		//	Assert.AreSame(target[0], fruits);
		//	Assert.AreSame(target[1], vegetables);
		//}
		[TestMethod]
		public void Can_indicate_Categories() {
			//Arrange
			var fruits = "Fruits";
			var vegetables = "Vegetables";
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1, Name = "Banana", Category = fruits},
				new Product {ProductId = 2, Name = "Potatos", Category = vegetables}
			});

			//Act
			var target = new NavController(mock.Object);
			var result = target.Menu(fruits).ViewBag.SelectedCategory;
			//Assert
			Assert.AreEqual(result, fruits);
		}
		[TestMethod]
		public void Generate_Specific_Category_Page_Counter() {
			//Arragne
			var fruits = "Fruits";
			var vegetables = "Vegetables";
			var berries = "Berries";
			mock.Setup(m => m.Products).Returns(new[]
			{
					new Product {ProductId = 1, Name = "Banana", Category = fruits},
					new Product {ProductId = 2, Name = "Apple", Category = fruits},
					new Product {ProductId = 4, Name = "Orange", Category = fruits},

					new Product {ProductId = 3, Name = "Potatos", Category = vegetables},
					new Product {ProductId = 5, Name = "Tomato", Category = vegetables},

					new Product {ProductId = 6, Name = "Blueberry", Category = berries}
				});
			//Act
			ProductController pc = new ProductController(mock.Object);
			int currentpage = 1;
			var fruitRes = ((ProducListViewModel)pc.List(fruits, currentpage).Model)
				.PagingInfo.ProductsCount;
			var vegetablesRes = ((ProducListViewModel)pc.List(vegetables, currentpage).Model)
				.PagingInfo.ProductsCount;
			var berriesRes = ((ProducListViewModel)pc.List(berries, currentpage).Model)
				.PagingInfo.ProductsCount;
			//Assert
			Assert.AreEqual(fruitRes,3);
			Assert.AreEqual(vegetablesRes, 2);
			Assert.AreEqual(berriesRes, 1);
		}
	}
}
