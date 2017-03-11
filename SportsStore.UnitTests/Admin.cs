using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using SportsStore.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
	[TestClass]
	public class Admin
	{
		[TestMethod]
		public void CanEditProduct() {
			//Arrange
			var mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products)
				.Returns(new Product[]
				{
					new Product {ProductId = 1, Name = "P1"},
					new Product {ProductId = 2, Name = "P2"},
					new Product {ProductId = 3, Name = "P3"}
				});
			AdminController target = new AdminController(mock.Object);

			//Act
			var p1 = target.Edit(1).ViewData.Model as Product;
			var p2 = target.Edit(2).ViewData.Model as Product;
			var p3 = target.Edit(3).ViewData.Model as Product;

			//Assert
			Assert.AreEqual(1, p1.ProductId);
			Assert.AreEqual(2, p2.ProductId);
			Assert.AreEqual(3, p3.ProductId);
		}
		[TestMethod]
		public void CanEdit_NoExistentProduct() {
			//Arrage
			var mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns(new[]
			{
				new Product {ProductId = 1, Name = "P1"},
				new Product {ProductId = 2, Name = "P2"},
				new Product {ProductId = 3, Name = "P3"}
			});

			AdminController target = new AdminController(mock.Object);
			//Act
			var product = target.Edit(4).ViewData.Model as Product;
			//Assert
			Assert.IsNull(product);
		}
	}
}
