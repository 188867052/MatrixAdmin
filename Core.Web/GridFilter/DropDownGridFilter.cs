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
        private readonly Expression<Func<TPostModel, int>> expression;
        public DropDownGridFilter(Expression<Func<TPostModel, int>> expression, string labelText, bool isContainsEmpty = default) : base(labelText)
        {
            this.Delegate = "alert(this.value)";
            this.Text = labelText;
            this.Event = new JavaScriptEvent(Delegate);
            this.IsContainsEmpty = isContainsEmpty;
            this.expression = expression;
            this.keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public bool IsContainsEmpty { get; set; }

        private readonly IList<KeyValuePair<int, string>> keyValuePair;

        public void AddOption(int key, string value)
        {
            keyValuePair.Add(new KeyValuePair<int, string>(key, value));
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }

        public string Text { get; set; }

        public override string Render()
        {
            string options = IsContainsEmpty ? "<option></option>" : default;
            string name = this.expression.GetPropertyInfo().Name;
            options = keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<label>{Text}</label>" +
                   $"<select name=\"{name}\">{options}</select>";
        }
    }
}