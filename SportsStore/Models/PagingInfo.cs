using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
	public class PagingInfo
	{
		public int ProductsCount { get; set; }
		public int ProductsPerPage { get; set; }
		public int CurrentPage { get; set; }

		public int TotalPages => (int) Math.Ceiling((decimal) ProductsCount/ProductsPerPage);
	}
}