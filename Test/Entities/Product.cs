using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
	public class Product
	{
		[HiddenInput(DisplayValue = false)]
		public int ProductId{get; set;}
		public string Name { get; set; }
		public string Description { get; set; }
		[DataType(DataType.MultilineText)]
		public string Category { get; set; }
		public decimal Price { get; set; }
	}
}