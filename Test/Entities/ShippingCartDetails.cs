﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
	public class ShippingCartDetails
	{
		[Required(ErrorMessage = "Please enter a name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter the first line address")]
		[DisplayName(displayName:"Line 1")]
		public string Line1 { get; set; }

		[DisplayName(displayName: "Line 2")]
		public string Line2 { get; set; }

		[DisplayName(displayName: "Line 3")]
		public string Line3 { get; set; }

		[Required(ErrorMessage = "Please enter a city name")]
		public string City { get; set; }
		[Required(ErrorMessage = "Please enter a state name")]
		public string State { get; set; }

		public string Zip { get; set; }
		[Required(ErrorMessage = "Please enter a country name")]
		public string Country { get; set; }

		public bool GiftWrap { get; set; }
	}
}
