using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
	[TestClass]
	public class ImageTest
	{
		[TestMethod]
		public void Can_Retrive_Image_Data() {
			//Arrange
			var product = new Product {
				ProductId = 2,
				Name = "Test",
				ImageData = new byte[] { },
				ImageMimeType = "image/png"
			};
			//Act
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1, Name = "P1"},
				product,
				new Product {ProductId = 3, Name = "P3"}
			}.AsQueryable());
			var target = new ProductController(mock.Object);
			var file = target.GetImage(product.ProductId);
			//Assert
			Assert.IsNotNull(file);
			Assert.IsInstanceOfType(file, typeof(FileResult));
			Assert.AreEqual(product.ImageMimeType, file.ContentType);
		}
		[TestMethod]
		public void CanNot_Retrive_Image_From_invalid_ProductId() {
			//Arrange
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1, Name = "P1"},
				new Product {ProductId = 2, Name = "P2"}
			}.AsQueryable());
			//Act
			var target = new ProductController(mock.Object);
			var file = target.GetImage(100);
			//Assert
			Assert.IsNull(file);
		}
	}
}
