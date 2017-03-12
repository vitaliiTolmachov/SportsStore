using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
	public interface IProductRepository
	{
		IEnumerable<Product> Products { get;}
		IEnumerable<SelectListItem> CategotyList { get;}
		void SaveProduct(Product product);
	}
}
