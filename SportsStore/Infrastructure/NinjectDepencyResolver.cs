using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace SportsStore.Infrastructure
{
    public class NinjectDepencyResolver :IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepencyResolver(IKernel paramKernel)
        {
            this.kernel = paramKernel;
            this.AddBindings();
        }

        private void AddBindings()
        {
            //My bindings will be here
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}