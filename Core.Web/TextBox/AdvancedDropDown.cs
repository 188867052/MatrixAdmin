using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.GridFilter;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.JavaScript;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.TextBox
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="TPostModel">The post model.</typeparam>
    /// <typeparam name="TModel">TModel.</typeparam>
    public class AdvancedDropDown<TPostModel, TModel> : IHtmlContent<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, Enum>> _expression;
        private readonly string _labelText;
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDown{TPostModel, TModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="methodCall">The method call.</param>
        public AdvancedDropDown(Expression<Func<TPostModel, Enum>> expression, string labelText, MethodCall methodCall)
        {
            this._expression = expression;
            this._labelText = labelText;
            this._script = new JavaScriptEvent(func: methodCall.Method, id: methodCall.Id, eventType: JavaScriptEventEnum.MouseDown).Render();
            this._id = methodCall.Id;
        }

        public TagHelperOutput Render(TModel model)
        {
            string listId = new Identifier().Value;
            string property = this._expression.GetPropertyName();

            var div = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "form-group" }, });
            var label = HtmlContent.TagHelper("label");
            label.Content.SetContent(this._labelText);
            var input = HtmlContent.TagHelper("input", new TagHelperAttributeList {
                { "class", "form-control" },
                { "id", this._id.Value },
                { "name", property },
                { "list", listId },
            });
            label.PostElement.AppendHtml(input);
            label.PostElement.AppendHtml(HtmlContent.TagHelper("select"));
            div.PostElement.AppendHtmlLine(this._script);
            div.PostElement.AppendHtml(HtmlContent.TagHelper("datalist", new TagHelperAttributeList { { "id", listId }, }));

            return div;
        }
    }
}