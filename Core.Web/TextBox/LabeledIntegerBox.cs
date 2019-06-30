using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.GridFilter;
using Core.Web.Html;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.TextBox
{
    public class LabeledIntegerBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, int?>> _expression;
        private readonly Expression<Func<TModel, int?>> _modelExpression;
        private readonly string _label;
        private readonly string type;

        public LabeledIntegerBox(string label, Expression<Func<TPostModel, int?>> expression, Expression<Func<TModel, int?>> modelExpression = null, TextBoxTypeEnum type = default)
        {
            this._expression = expression;
            this._modelExpression = modelExpression;
            this._label = label;
            this.type = JavaScriptEnumMappings.ToString(type);
        }

        public TagHelperOutput Render(TModel entity)
        {
            string id = new Identifier().Value;
            string name = this._expression.GetPropertyName();
            string value = default;
            if (entity != null)
            {
                value = this._modelExpression.Compile()(entity).ToString();
            }

            var div = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "form-group" }, });
            var label = HtmlContentUtilities.MakeTagHelperOutput("label", new TagHelperAttributeList { { "for", id }, });
            label.Content.SetContent(this._label + ":");
            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "type", this.type },
                { "name", name },
                { "value", value },
                { "id", id },
            };
            label.PostElement.AppendHtml(HtmlContentUtilities.MakeTagHelperOutput("input", attributes));
            div.Content.AppendHtml(label);
            return div;
        }
    }
}