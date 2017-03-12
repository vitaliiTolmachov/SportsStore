using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using SportsStore.Infrastructure.Abstract;
using SportsStore.Infrastructure.Concrete;

namespace SportsStore.Infrastructure
{
    public class NinjectDepencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepencyResolver(IKernel paramKernel) {
            this.kernel = paramKernel;
            this.AddBindings();
        }


        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings() {
            //My bindings will be here
            //This binding returns data from DB using my own realization of DBContext
            this.kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings {
                WriteAsFile = Boolean.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            //This mock returns constant collection of Products
            //var mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Fooball", Price = 25},
            //    new Product {Name = "Surfboard", Price = 50},
            //    new Product {Name = "RuningShoes", Price = 15},
            //    new Product {Name = "Snowboard", Price = 100}
            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

	        kernel.Bind<IAuthProvider>().To<AuthProvider>();
        }
}
}