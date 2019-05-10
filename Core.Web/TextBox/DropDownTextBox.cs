using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Html;

namespace Core.Web.TextBox
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="TPostModel">The post model.</typeparam>
    /// <typeparam name="TModel">The enum.</typeparam>
    public class DropDownTextBox<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;
        private readonly string _labelText;
        private readonly Expression<Func<TPostModel, Enum>> _expression;
        private int _selectedKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownTextBox{TPostModel, TEnumType}"/> class.
        /// </summary>
        /// <param name="labelText">The labelText.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="isContainsEmpty">The isContainsEmpty.</param>
        public DropDownTextBox(string labelText, Expression<Func<TPostModel, Enum>> expression, bool isContainsEmpty = true)
        {
            this._isContainsEmpty = isContainsEmpty;
            this._labelText = labelText;
            this._expression = expression;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(int key, string value, bool isSelected = false)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>(key, value));
            if (isSelected)
            {
                this._selectedKey = key;
            }
        }

        public string Render(TModel model)
        {
            string property = this._expression.GetPropertyName();
            string options = this._isContainsEmpty ? "<option></option>" : default;
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}' {(this._selectedKey == item.Key ? "selected=\"selected\"" : string.Empty)}>{item.Value}</option>");

            return
                $"<div class=\"form-group\">" +
                $"<label>{this._labelText}</label>" +
                $"<select class=\"form-control\" name=\"{property}\">" +
                $"{options}" +
                $"</select>" +
                $"</div>";
        }
    }
}