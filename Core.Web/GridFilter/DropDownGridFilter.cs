using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="T">The post model.</typeparam>
    /// <typeparam name="TEnumType">The enum.</typeparam>
    public class DropDownGridFilter<T, TEnumType> : BaseGridFilter where TEnumType : Enum
    {
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownGridFilter{TPostModel, TEnumType}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="isContainsEmpty">The isContainsEmpty.</param>
        public DropDownGridFilter(Expression<Func<T, TEnumType>> expression, string labelText, bool isContainsEmpty = true) : base(labelText, expression.GetPropertyName())
        {
            this._isContainsEmpty = isContainsEmpty;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(TEnumType key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public override string Render()
        {
            string options = this._isContainsEmpty ? "<option></option>" : default;
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<select class=\"form-control\" style=\"width:204.16px\" name=\"{this.InputName}\">" +
                   $"{options}" +
                   $"</select>" +
                   $"</div>" +
                   $"</div>";
        }
    }
}