using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
	public class EFProductRepository:IProductRepository
	{
		private EFDBContext dbContext = new EFDBContext();

		public IEnumerable<Product> Products => dbContext.Products;

		public SelectList CategotyList
		{
			get { return new SelectList(dbContext.Products.Select(p => p.Category).Distinct().ToList()); }
		}

		public void SaveProduct(Product product)
		{
			if (product.ProductId.Equals(0)) {
				dbContext.Products.Add(product);
			}else
			{
				var p = dbContext.Products.Find(product.ProductId);
				p.Description = product.Description;
				p.Name = product.Name;
				p.Category = product.Category;
				p.Price = product.Price;
			}
			dbContext.SaveChanges();
		}
	}
}
