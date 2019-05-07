using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;

namespace Core.Web.TextBox
{
    public class LabeledTextBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
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
            this.type = EnumMappings.ToString(type);
        }

        public string Render(TModel entity)
        {
            string id = new Identifier().Value;
            string value = default;
            if (entity != null)
            {
                value = this._modelExpression.Compile()(entity);
            }

            string name = this._expression.GetPropertyName();
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"{id}\">{this._label}:</label>" +
                          $"<input type=\"{this.type}\" name=\"{name}\" class=\"form-control\" value=\"{value}\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }

    public class HiddenTextBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, int>> _expression;
        private readonly int _value;

        public HiddenTextBox(Expression<Func<TPostModel, int>> expression, int value)
        {
            this._expression = expression;
            this._value = value;
        }

        public string Render(TModel model)
        {
            string name = this._expression.GetPropertyName();
            return $"<input type=\"hidden\" name=\"{name}\" value=\"{this._value}\">";
        }
    }
}