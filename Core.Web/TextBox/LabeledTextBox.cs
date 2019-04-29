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

        public LabeledTextBox(Expression<Func<TPostModel, string>> expression, string label)
        {
            this._expression = expression;
            this._label = label;
        }

        public string Render()
        {
            string id = new Identifier().Value;
            string name = _expression.GetPropertyName();
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"{id}\">{_label}:</label>" +
                          $"<input type=\"password\" name=\"{name}\" class=\"form-control\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }
}