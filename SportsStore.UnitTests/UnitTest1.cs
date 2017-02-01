using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

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
			var result = (IEnumerable<Product>)controller.List(currentPage: 2).Model;

			//Assert
			var productsFromPage2 = result.ToArray();
			Assert.AreEqual(2, productsFromPage2.Length);
			Assert.IsTrue(productsFromPage2[0].ProductId == 4);
			Assert.IsTrue(productsFromPage2[1].ProductId == 5);
		}
	}
}
