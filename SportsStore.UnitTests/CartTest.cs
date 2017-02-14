using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Models;

namespace SportsStore.UnitTests
{
	[TestClass]
	public class CartTest
	{
		[TestMethod]
		public void Can_Add_New_Lines() {
			//Arrange
			var product1 = new Product { ProductId = 1, Name = "P1" };
			var product2 = new Product { ProductId = 2, Name = "P2" };
			//Act
			var cart = new Cart();
			cart.Add(product1, 1);
			cart.Add(product2, 2);
			var result = cart.Lines.ToArray();

			//Assert
			Assert.AreEqual(result.Length, 2);
			Assert.AreEqual(result[0].Quantity, 1);
			Assert.AreEqual(result[1].Quantity, 2);
			Assert.AreEqual(result[0].Product, product1);
			Assert.AreEqual(result[1].Product, product2);

		}
		[TestMethod]
		public void Can_Add_Existing_Products_to_CartLine() {
			//Arrange
			var product1 = new Product { ProductId = 1, Name = "P1" };
			var product2 = new Product { ProductId = 2, Name = "P2" };
			var cartline = new Cart();
			//Act
			cartline.Add(product1, 1);
			cartline.Add(product2, 2);
			cartline.Add(product1, 10);
			var res = cartline.Lines.ToArray();
			//Assert
			Assert.AreEqual(res[0].Quantity, 11);
			Assert.AreEqual(res.Length, 2);
		}
		[TestMethod]
		public void Can_Delete_Product() {
			//Arrange
			var product1 = new Product { ProductId = 1, Name = "P1" };
			var product2 = new Product { ProductId = 2, Name = "P2" };
			var cartline = new Cart();
			//Act
			cartline.Add(product1, 2);
			cartline.Add(product2, 4);
			cartline.Add(product1, 10);
			cartline.Remove(product1);
			var res = cartline.Lines.ToArray();
			//Assert
			Assert.AreEqual(res.Length, 1);
			Assert.AreEqual(res[0].Product, product2);
			Assert.AreEqual(cartline.Lines.Where(p => p.Product.ProductId == 1).Count(), 0);
		}
		[TestMethod]
		public void Calculate_Total() {
			//Arrange
			var product1 = new Product { ProductId = 1, Name = "P1", Price = 50 };
			var product2 = new Product { ProductId = 2, Name = "P2", Price = 20 };
			var cartline = new Cart();
			//Act
			cartline.Add(product1, 2);
			cartline.Add(product2, 4);
			cartline.Add(product1, 10);
			var res = cartline.GetTotal();
			//Assert
			Assert.AreEqual(res, 680);
		}
		[TestMethod]
		public void Can_Clear_All() {
			//Arrange
			var product1 = new Product { ProductId = 1, Name = "P1", Price = 50 };
			var product2 = new Product { ProductId = 2, Name = "P2", Price = 20 };
			var cartline = new Cart();
			//Act
			cartline.Add(product1, 2);
			cartline.Add(product2, 4);
			cartline.Add(product1, 10);
			cartline.Clear();
			//Assert
			Assert.AreEqual(cartline.Lines.Count(), 0);
		}
		[TestMethod]
		public void Can_Add_To_Cart() {
			//Arrange
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1, Category = "Apple", Name = "P1"}
			}.AsQueryable());
			Cart cart = new Cart();
			var target = new CartController(mock.Object);
			//Act
			target.AddToCart(cart, 1, null);
			target.AddToCart(cart, 1, null);
			//Assert
			Assert.AreEqual(cart.Lines.Count(), 1);
			Assert.AreEqual(cart.Lines.FirstOrDefault().Product.ProductId, 1);
		}
		[TestMethod]
		public void Adding_ToCart_Goes_To_Card_Screen() {
			//Arrange
			var mock = new Mock<IProductRepository>();
			mock.Setup(repo => repo.Products).Returns(new[]
			{
				new Product {ProductId = 1, Name = "P1", Category = "Apples"}
			}.AsQueryable());
			var cart = new Cart();
			var target = new CartController(mock.Object);
			//Act
			var result = target.AddToCart(cart, 1, "returnUrl");
			//Assert
			Assert.AreEqual(result.RouteValues["action"],"Index");
			Assert.AreEqual(result.RouteValues["returnUrl"], "returnUrl");
		}
		[TestMethod]
		public void Can_View_Cart_Content() {
			//Arrange
			var cart = new Cart();
			var cartcontroller = new CartController(null);
			//Act
			var reuslt = (CartIndexViewModel)cartcontroller.Index(cart, "myUrl").ViewData.Model;
			//Assert
			Assert.AreSame(reuslt.Cart, cart);
			Assert.AreEqual(reuslt.ReturnUrl, "myUrl");


		}
	}
}
