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
        private readonly Expression<Func<TPostModel, Enum>> expression;
        private readonly IList<KeyValuePair<int, string>> keyValuePair;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="isContainsEmpty"></param>
        public DropDownGridFilter(Expression<Func<TPostModel, Enum>> expression, string labelText, bool isContainsEmpty = default) : base(labelText)
        {
            this.Delegate = "alert(this.value)";
            this.Text = labelText;
            this.Event = new JavaScriptEvent(Delegate);
            this.IsContainsEmpty = isContainsEmpty;
            this.expression = expression;
            this.keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public bool IsContainsEmpty { get; set; }

        private void AddOption(int key, string value)
        {
            keyValuePair.Add(new KeyValuePair<int, string>(key, value));
        }

        public void AddOption(Enum key, string value)
        {
            keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }

        public string Text { get; set; }

        public override string Render()
        {
            string options = IsContainsEmpty ? "<option></option>" : default;
            string name = this.expression.GetPropertyInfo().Name;
            options = keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"custom-control-inline\">" +
                   $"<label>{Text}</label>" +
                   $"<select name=\"{name}\">{options}</select>" +
                   $"</div>";
        }
    }
}