using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Models;
using FS.Store.WebAPI.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FS.Store.WebAPI.TagHelpers
{
    [HtmlTargetElement("th", Attributes = "table-header-ordered")]
    public class TableHeaderOrderedTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = default!;
        public string Field { get; set; } = null!;
        public string? HeaderText { get; set; }
        public string? Classes { get; set; }
        public string? IconClasses { get; set; }
        public bool IsNumber { get; set; }
        public int? Colspan { get; set; }
        public int? Rowspan { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var query = ViewContext.HttpContext.Request.Query;

            string? orderByText = query[$"OrderBy{Field}"].ToString();

            OrderBy orderBy = OrderByUtils.GetOrderBy(orderByText, IsNumber);

            output.TagName = "th";

            var existingClasses = output.Attributes["class"]?.Value?.ToString();
            var classes = new[]
            {
                existingClasses,
                Classes,
                IsNumber ? "numericOrder" : "alphaOrder"
            };
            output.Attributes.SetAttribute("class", string.Join(" ", classes.Where(c => c.IsNotNullOrEmpty())));

            output.Attributes.SetAttribute("data-field", Field);

            output.Attributes.SetAttribute("data-order", orderBy.Text ?? "");

            output.Attributes.SetAttribute("data-order-id", orderBy.Id);

            if (Colspan.HasValue)
                output.Attributes.SetAttribute("colspan", Colspan.Value);

            if (Rowspan.HasValue)
                output.Attributes.SetAttribute("rowspan", Rowspan.Value);

            var content = new HtmlContentBuilder();

            content.Append(HeaderText ?? Field);

            var icon = new TagBuilder("i");

            icon.AddCssClass("bi");
            icon.AddCssClass("notranslate");
            icon.AddCssClass("order-by");

            if (IconClasses.IsNotNullOrEmpty())
                icon.AddCssClass(IconClasses);

            icon.AddCssClass(orderBy.Icon ?? "bi-arrow-down-up");

            content.AppendHtml(" ");
            content.AppendHtml(icon);

            output.Content.SetHtmlContent(content);
        }
    }
}
