using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;

namespace SportsStore.Models
{
	public class ProducListViewModel
	{
		public PagingInfo PagingInfo { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}