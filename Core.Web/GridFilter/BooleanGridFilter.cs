using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class BooleanGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, bool?>> _expression;
        private readonly IList<KeyValuePair<bool, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText"></param>
        /// <param name="isContainsEmpty"></param>
        public BooleanGridFilter(Expression<Func<TPostModel, bool?>> expression, string labelText, bool isContainsEmpty = true) : base(labelText, expression.GetPropertyName())
        {
            this._expression = expression;
            this._keyValuePair = new List<KeyValuePair<bool, string>>();
            this._isContainsEmpty = isContainsEmpty;
        }

        public void AddOption(Enum key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<bool, string>((bool)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public void AddOption(bool key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<bool, string>(key, value));
        }

        public override string Render()
        {
            if (this._keyValuePair.Count == 0)
            {
                this.SetDefaultOptions();
            }

            string options = this._isContainsEmpty ? "<option></option>" : default;
            string name = this._expression.GetPropertyName();
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<select class=\"form-control\" style=\"width:204.16px\" name=\"{name}\">{options}</select>" +
                   $"</div>" +
                   $"</div>";
        }

        /// <summary>
        /// Set default options.
        /// </summary>
        private void SetDefaultOptions()
        {
            this._keyValuePair.Add(new KeyValuePair<bool, string>(true, "是"));
            this._keyValuePair.Add(new KeyValuePair<bool, string>(false, "否"));
        }
    }
}