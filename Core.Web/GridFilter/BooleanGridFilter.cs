using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridFilter
{
    public class BooleanGridFilter<T> : BaseGridFilter
    {
        private readonly IList<KeyValuePair<bool, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanGridFilter{TPostModel}"/> class.
        /// 构造函数.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">labelText.</param>
        /// <param name="isContainsEmpty">isContainsEmpty.</param>
        /// <param name="tooltip">toolTip.</param>
        public BooleanGridFilter(Expression<Func<T, bool?>> expression, string labelText, bool isContainsEmpty = true, string tooltip = default) : base(labelText, expression.GetPropertyName(), tooltip: tooltip)
        {
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
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label {this.Tooltip}>{this.LabelText}</label>" +
                   $"<select class=\"form-control\" style=\"width:204.16px\" name=\"{this.InputName}\">{options}</select>" +
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