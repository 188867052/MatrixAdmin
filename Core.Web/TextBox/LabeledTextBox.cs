using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Identifiers;

namespace Core.Web.TextBox
{
    public class LabeledTextBox<TPostModel>
    {
        private readonly Expression<Func<TPostModel, string>> _expression;
        private readonly string _label;
        private readonly string type;

        public LabeledTextBox(Expression<Func<TPostModel, string>> expression, string label, TextBoxTypeEnum type = default)
        {
            this._expression = expression;
            this._label = label;
            this.type = type.ToString();
        }

        public string Render()
        {
            string id = new Identifier().Value;
            string name = this._expression.GetPropertyName();
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"{id}\">{_label}:</label>" +
                          $"<input type=\"{type}\" name=\"{name}\" class=\"form-control\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }
}