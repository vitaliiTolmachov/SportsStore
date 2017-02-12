using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;

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
			var product1 = new Product { ProductId = 1, Name = "P1",Price = 50};
			var product2 = new Product { ProductId = 2, Name = "P2",Price = 20};
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
	}
}
