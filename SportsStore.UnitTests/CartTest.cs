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
            var target = new CartController(mock.Object, null);
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
            var target = new CartController(mock.Object, null);
            //Act
            var result = target.AddToCart(cart, 1, "returnUrl");
            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "returnUrl");
        }
        [TestMethod]
        public void Can_View_Cart_Content() {
            //Arrange
            var cart = new Cart();
            var cartcontroller = new CartController(null,null);
            //Act
            var reuslt = (CartIndexViewModel)cartcontroller.Index(cart, "myUrl").ViewData.Model;
            //Assert
            Assert.AreSame(reuslt.Cart, cart);
            Assert.AreEqual(reuslt.ReturnUrl, "myUrl");


        }
        [TestMethod]
        public void Can_Checkout_Empty_Cart() {
            //Arrange
            //Order Handler
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            var shippingDetails = new ShippingCartDetails();
            CartController cartController = new CartController(null, mock.Object);

            //Act
            ViewResult result = cartController.Checkout(cart, shippingDetails);
            //Заказ не был передан обработчику
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingCartDetails>()),
                Times.Never());

            //Assert
            //We dont't go anywere
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails() {
            //Arrange
            var mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.Add(new Product(), 1);
            CartController cartController = new CartController(null, mock.Object);
            cartController.ModelState.AddModelError("error","error");

            //Act
            ViewResult view = cartController.Checkout(cart, new ShippingCartDetails());

            //Assert
            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingCartDetails>()),
                Times.Never);
            Assert.AreEqual(string.Empty, view.ViewName);
            Assert.AreEqual(false, view.ViewData.ModelState.IsValid);

        }
        [TestMethod]
        public void Can_Checkout_And_Submit_Order() {
            //Arrange
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            cart.Add(new Product(), 1);
            CartController cartController = new CartController(null,mock.Object);

            //Act
            ViewResult view = cartController.Checkout(cart, new ShippingCartDetails());

            //Assert
            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingCartDetails>()),
                Times.Once);
            Assert.AreEqual("Completed", view.ViewName);
            Assert.AreEqual(true, view.ViewData.ModelState.IsValid);
        }
    }
}
