using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;
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
            this.type = type.ToString();
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
                          $"<label for=\"{id}\">{_label}:</label>" +
                          $"<input type=\"{type}\" name=\"{name}\" class=\"form-control\" value=\"{value}\" id=\"{id}\">" +
                          $"</div>";
            return html;
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public class DropDownTextBox<TPostModel, TEnumType> : ITextRender<TPostModel, TEnumType>
    {
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="isContainsEmpty"></param>
        public DropDownTextBox(string labelText, bool isContainsEmpty = true)
        {
            this._isContainsEmpty = isContainsEmpty;
            this.labelText = labelText;

            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(TEnumType key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public void AddOption(int key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>(key, value));
        }

        public string InputName = "InputName";
        private string labelText;

        public string Render(TEnumType model)
        {
            string options = _isContainsEmpty ? "<option></option>" : default;
            options = _keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return
                $"<div class=\"form-group\">" +
                $"<label>{this.labelText}</label>" +
                $"<select class=\"form-control\" name=\"{InputName}\">" +
                $"{options}" +
                $"</select>" +
                $"</div>";
        }
    }
}