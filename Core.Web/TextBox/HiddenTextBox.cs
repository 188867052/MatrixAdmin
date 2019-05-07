using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Enums;
using Core.Web.Html;

namespace Core.Web.TextBox
{
    public class HiddenTextBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, int>> _expression;
        private readonly int _value;
        private readonly string type;

        public HiddenTextBox(Expression<Func<TPostModel, int>> expression, int value)
        {
            this._expression = expression;
            this._value = value;
            this.type = EnumMappings.ToString(TextBoxTypeEnum.Hidden);
        }

        public string Render(TModel model)
        {
            string name = this._expression.GetPropertyName();
            return $"<input type=\"{this.type}\" name=\"{name}\" value=\"{this._value}\">";
        }
    }
}