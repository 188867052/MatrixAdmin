using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<T> : BaseGridFilter
    {
        public DateTimeGridFilter(Expression<Func<T, DateTime?>> expression, string label, string tooltip = default) : base(label, expression.GetPropertyName(), tooltip: tooltip)
        {
        }

        public override string Render()
        {
            string id = new Identifier().Value;
            var div = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", this.ContainerClass }, });
            var divGroup = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "form-group" }, });

            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };
            var label = HtmlContentUtilities.MakeTagHelperOutput("label", labelAttributes);

            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form_datetime form-control" },
                { "id", id },
                { "name", this.InputName },
                { "type", "text" },
            };

            var input = HtmlContentUtilities.MakeTagHelperOutput("input", inputAttributes);
            div.Content.SetHtmlContent(divGroup);
            divGroup.Content.SetHtmlContent(label);
            label.Content.SetContent(this.LabelText);
            label.PostElement.AppendHtml(input);
            var html = HtmlContentUtilities.HtmlContentToString(div);
            return html;
        }
    }
}