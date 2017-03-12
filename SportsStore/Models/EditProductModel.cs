using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.Models
{
	public class EditProductModel
	{
		public SelectList CategoryList{get; set;}
		public Product Product { get; set; }
	}
}