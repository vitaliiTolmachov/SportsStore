using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Entities
{
	public class CartLine
	{
		public Product Product{get; set;}
		public int Quantity{get; set;}
	}
	public class Cart
	{
		private List<CartLine> _lineCollecction = new List<CartLine>();
		public IEnumerable<CartLine> Lines => _lineCollecction;
		public void Add(Product product, int quantity) {
			CartLine line = Lines.Where(p => p.Product.ProductId.Equals(product.ProductId))
				.FirstOrDefault();

			//when adding first product
			if (line == null) {
				this._lineCollecction.Add(new CartLine {
					Product = product,
					Quantity = quantity
				});
			} else {
				line.Quantity += quantity;
			}
		}
		public void Remove(Product product) {
			this._lineCollecction.RemoveAll(p => p.Product.ProductId.Equals(product.ProductId));
		}
		public void Clear() {
			this._lineCollecction.Clear();
		}
		public decimal GetTotal() {
			return this._lineCollecction.Sum(p => p.Product.Price * p.Quantity);
		}
	}
}
