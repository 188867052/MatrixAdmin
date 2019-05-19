using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;

namespace Core.Web.TextBox
{
    public class LabeledIntegerTextBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, int?>> _expression;
        private readonly Expression<Func<TModel, int?>> _modelExpression;
        private readonly string _label;
        private readonly string type;

        public LabeledIntegerTextBox(string label, Expression<Func<TPostModel, int?>> expression, Expression<Func<TModel, int?>> modelExpression = null, TextBoxTypeEnum type = default)
        {
            this._expression = expression;
            this._modelExpression = modelExpression;
            this._label = label;
            this.type = JavaScriptEnumMappings.ToString(type);
        }

        public string Render(TModel entity)
        {
            string id = new Identifier().Value;
            string value = default;
            if (entity != null)
            {
                value = this._modelExpression.Compile()(entity).ToString();
            }

            string name = this._expression.GetPropertyName();
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"{id}\">{this._label}:</label>" +
                          $"<input type=\"{this.type}\" name=\"{name}\" class=\"form-control\" value=\"{value}\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }
}