using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SportsStore.Models;

namespace SportsStore.HTMLHelpers
{
	public static class PagingHelper
	{
		public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper,
										PagingInfo pagingInfo,
										Func<int, string> pageUrlFunc) {
			var sb = new StringBuilder();
			for (int i = 1; i < pagingInfo.TotalPages; i++) {
				var tag = new TagBuilder("a");
				tag.MergeAttribute("href", pageUrlFunc.Invoke(i));
				tag.InnerHtml = string.Format("Page {0} ", i);
				//mark current page
				if (i.Equals(pagingInfo.CurrentPage)) {
					tag.AddCssClass("selected");
					tag.AddCssClass("btn-primary");
				}
				tag.AddCssClass("btn btn-default");
				sb.Append(tag);
			}
			return MvcHtmlString.Create(sb.ToString());
		}
	}
}