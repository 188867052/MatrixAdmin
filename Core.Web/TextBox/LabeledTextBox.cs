using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Identifiers;

namespace Core.Web.TextBox
{
    public class LabeledTextBox<TPostModel, T>
    {
        private readonly Expression<Func<TPostModel, string>> _expression;
        private readonly Expression<Func<T, string>> _modelExpression;
        private readonly string _label;
        private readonly string type;

        public LabeledTextBox(string label, Expression<Func<TPostModel, string>> expression, Expression<Func<T, string>> modelExpression = null, TextBoxTypeEnum type = default)
        {
            this._expression = expression;
            this._modelExpression = modelExpression;
            this._label = label;
            this.type = type.ToString();
        }

        public string Render(T entity)
        {
            string id = new Identifier().Value;
            string value = default;
            if (entity != null)
            {
                value = this._modelExpression.Compile()(entity);
            }
            string name = this._expression.GetPropertyName();
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"{id}\">{_label}:</label>" +
                          $"<input type=\"{type}\" name=\"{name}\" class=\"form-control\" value=\"{value}\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }
}