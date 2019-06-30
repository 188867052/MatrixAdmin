using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.GridFilter;
using Core.Web.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.TextBox
{
    public class HiddenValue<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, int>> _expression;
        private readonly int _value;
        private readonly string _type;

        public HiddenValue(Expression<Func<TPostModel, int>> expression, int value)
        {
            this._expression = expression;
            this._value = value;
            this._type = JavaScriptEnumMappings.ToString(TextBoxTypeEnum.Hidden);
        }

        public string Render(TModel model)
        {
            string name = this._expression.GetPropertyName();
            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                { "type", this._type },
                { "name", name },
                { "value", this._value },
            };

            var div = HtmlContentUtilities.MakeTagHelperOutput("input", attributes);
            return HtmlContentUtilities.HtmlContentToString(div);
        }
    }
}