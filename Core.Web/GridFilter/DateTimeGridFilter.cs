using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Html;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<T> : BaseGridFilter
    {
        public DateTimeGridFilter(Expression<Func<T, DateTime?>> expression, string label, string tooltip = default) : base(label, expression.GetPropertyName(), tooltip: tooltip)
        {
        }

        public override TagHelperOutput Render()
        {
            string id = new Identifier().Value;
            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };

            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form_datetime form-control" },
                { "id", id },
                { "name", this.InputName },
                { "type", "text" },
            };

            var label = HtmlContent.TagHelper("label", labelAttributes, this.LabelText);
            var input = HtmlContent.TagHelper("input", inputAttributes);
            var divGroup = HtmlContent.TagHelper("div", new TagHelperAttribute("class", "form-group"), label, input);
            return HtmlContent.TagHelper("div", new TagHelperAttribute("class", this.ContainerClass), divGroup);
        }
    }
}