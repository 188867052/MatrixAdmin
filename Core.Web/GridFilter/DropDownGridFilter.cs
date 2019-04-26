using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public class DropDownGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, Enum>> _expression;
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="isContainsEmpty"></param>
        public DropDownGridFilter(Expression<Func<TPostModel, Enum>> expression, string labelText, bool isContainsEmpty = default) : base(labelText)
        {
            //this.Delegate = "alert(this.value)";
            //this.Event = new JavaScriptEvent(Delegate);
            this._isContainsEmpty = isContainsEmpty;
            this._expression = expression;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(Enum key, string value)
        {
            _keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        //private JavaScriptEvent Event { get; }

        //private string Delegate { get; }

        public override string Render()
        {
            string options = _isContainsEmpty ? "<option></option>" : default;
            string name = this._expression.GetPropertyInfo().Name;
            options = _keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<label>{LabelText}</label>" +
                   $"<select name=\"{name}\">{options}</select>" +
                   $"</div>";
        }
    }
}