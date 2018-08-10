using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreWiki.RazorPages.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private IUrlHelperFactory _UrlHelperFactory;

        public override int Order => base.Order;

        public PagerTagHelper(IUrlHelperFactory urlFactory)
        {
            _UrlHelperFactory = urlFactory;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var _urlHelper = _UrlHelperFactory.GetUrlHelper(this.ActionContext);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";

            if (!output.Attributes.ContainsName("class"))
            {
                output.Attributes.Add("class", "pagination");
            }
            else
            {
                output.Attributes.SetAttribute("class", output.Attributes["class"].Value + "pagination");
            }

            for (var pageNum = 1; pageNum <= TotalPages; pageNum++)
            {
                output.Content.AppendHtml($"<li class=\"page-item");
                if (pageNum == CurrentPage)
                {
                    output.Content.AppendHtml(" active");
                }
                output.Content.AppendHtml("\">");

                // A Tag
                if (pageNum == CurrentPage)
                {
                    output.Content.AppendHtml($"<a class=\"page-link active\" href=\"#\">{pageNum}</a>");
                }
                else
                {
                    var routes = new { PageNumber = pageNum };
                    output.Content.AppendHtml($"<a class=\"page-link\" href=\"{_urlHelper.Page("./All", routes)}\">{pageNum}</a>");
                }
            }

            output.Content.AppendHtml("</li>");
            return Task.CompletedTask;
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        [ViewContext]
        public ActionContext ActionContext { get; set; }
    }
}
