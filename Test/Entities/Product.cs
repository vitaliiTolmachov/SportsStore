using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
	public class Product
	{
		[HiddenInput(DisplayValue = true)]
		[Editable(false)]
		public int ProductId{get; set;}
		[Required(AllowEmptyStrings = false,ErrorMessage = "Please enter a product name")]
		public string Name { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a product description")]
		public string Description { get; set; }
		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Please enter a product description")]
		public string Category { get; set; }
		[Required]
		[Range(0.01,Double.MaxValue,ErrorMessage = "Please enter a positive price")]
		public decimal Price { get; set; }
	}
}