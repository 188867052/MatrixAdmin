using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.TextBox
{
    public class LabeledTextBox<TPostModel, TModel> : IHtmlContent<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, string>> _expression;
        private readonly Expression<Func<TModel, string>> _modelExpression;
        private readonly string _label;
        private readonly string type;

        public LabeledTextBox(string label, Expression<Func<TPostModel, string>> expression, Expression<Func<TModel, string>> modelExpression = null, TextBoxTypeEnum type = default)
        {
            this._expression = expression;
            this._modelExpression = modelExpression;
            this._label = label;
            this.type = JavaScriptEnumMappings.ToString(type);
        }

        public TagHelperOutput Render(TModel entity)
        {
            string id = new Identifier().Value;
            string value = default;
            string name = this._expression.GetPropertyName();
            if (entity != null)
            {
                value = this._modelExpression.Compile()(entity);
            }

            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "type", this.type },
                { "name", name },
                { "value", value },
                { "id", id },
            };

            var label = HtmlContent.TagHelper("label", new TagHelperAttribute("for", id), this._label + ":");
            var input = HtmlContent.TagHelper("input", attributes);

            return HtmlContent.TagHelper("div", new TagHelperAttribute("class", "form-group"), label, input);
        }
    }
}