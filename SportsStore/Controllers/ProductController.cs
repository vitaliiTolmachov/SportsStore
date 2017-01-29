using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository paramRepository)
        {
            this._repository = paramRepository;
        }
        // GET: Product
        public ViewResult List()
        {
            return View(_repository.Products);
        }
    }
}