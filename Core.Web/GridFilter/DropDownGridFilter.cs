using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public class DropDownGridFilter<TPostModel, TEnumType> : BaseGridFilter where TEnumType : Enum
    {
        private readonly Expression<Func<TPostModel, TEnumType>> _expression;
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="isContainsEmpty"></param>
        public DropDownGridFilter(Expression<Func<TPostModel, TEnumType>> expression, string labelText, bool isContainsEmpty = true) : base(labelText)
        {
            this._isContainsEmpty = isContainsEmpty;
            this._expression = expression;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(TEnumType key, string value)
        {
            _keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }


        public override string Render()
        {
            string options = _isContainsEmpty ? "<option></option>" : default;
            string name = this._expression.GetPropertyName();
            options = _keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<select class=\"form-control\" style=\"width:204.16px\" name=\"{name}\">" +
                   $"{options}" +
                   $"</select>" +
                   $"</div>" +
                   $"</div>";
        }
    }
}