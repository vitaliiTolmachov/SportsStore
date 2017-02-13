using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace SportsStore.Infrastructure.Binders
{
    public class CartModelBinder: System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            
            //Получить ссылку из сеанса
            if (controllerContext.HttpContext.Session!=null)
            {
                cart = (Cart) controllerContext.HttpContext.Session["Cart"];
            }

            //Эсли нету в данных сеанса то создать и поместить в данные сеанса
            if (cart == null && controllerContext.HttpContext.Session != null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session["Cart"] = cart;
            }

            return cart;
        }
    }
}