using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.JavaScript;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="T">The post model.</typeparam>
    public class AdvancedDropDownGridFilter<T> : BaseGridFilter
    {
        private readonly string _url;
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDownGridFilter{T}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="methodCall">The method call.</param>
        /// <param name="url">url.</param>
        public AdvancedDropDownGridFilter(Expression<Func<T, int?>> expression, string labelText, MethodCall methodCall, string url) : base(labelText, expression.GetPropertyName())
        {
            this._url = url;
            this._script = new JavaScriptEvent(func: methodCall.Method, methodCall.Id, eventType: JavaScriptEventEnum.MouseDown).Render();
            this._id = methodCall.Id;
        }

        public override TagHelperOutput Render()
        {
            string listId = new Identifier().Value;
            var div = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", this.ContainerClass }, });
            var divGroup = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "form-group" }, });
            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };
            var label = HtmlContent.TagHelper("label", labelAttributes);
            label.Content.SetContent(this.LabelText);
            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "id", this._id.Value },
                { "name", this.InputName },
                { "data-url", this._url },
                { "list", listId },
            };
            var input = HtmlContent.TagHelper("input", inputAttributes);

            div.Content.SetHtmlContent(divGroup);
            div.Content.AppendHtml(this._script);
            div.Content.AppendHtml(HtmlContent.TagHelper("datalist", new TagHelperAttributeList { { "id", listId }, }));
            divGroup.Content.SetHtmlContent(label);
            label.Content.SetContent(this.LabelText);
            label.PostElement.AppendHtml(input);
            return div;
        }
    }
}