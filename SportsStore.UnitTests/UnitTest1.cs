using System;
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
		[TestMethod]
		public void CanPaginate() {
			//Arrange
			var mock = new Mock<IProductRepository>();
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
			var result = (ProducListViewModel)controller.List(currentPage: 2).Model;

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
			var mock = new Mock<IProductRepository>();
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
			var result = (ProducListViewModel)controller.List(currentPage: 2).Model;

			//Assert
			PagingInfo page = result.PagingInfo;
			Assert.AreEqual(page.CurrentPage, 2);
			Assert.AreEqual(page.ProductsPerPage, 3);
			Assert.AreEqual(page.TotalPages, 2);
		}
	}
}
