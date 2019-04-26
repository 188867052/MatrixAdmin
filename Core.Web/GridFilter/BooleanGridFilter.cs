using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class BooleanGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, bool>> _expression;
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="isNotContainsEmpty"></param>
        public BooleanGridFilter(Expression<Func<TPostModel, bool>> expression, string labelText, bool isNotContainsEmpty = default) : base(labelText)
        {
            this._expression = expression;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
            this._isContainsEmpty = isNotContainsEmpty;
        }

        public void AddOption(Enum key, string value)
        {
            _keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public void AddOption(int key, string value)
        {
            _keyValuePair.Add(new KeyValuePair<int, string>(key, value));
        }

        public override string Render()
        {
            if (this._keyValuePair.Count == 0)
            {
                this.SetDefaultOptions();
            }

            string options = _isContainsEmpty ? default : "<option></option>";
            string name = this._expression.GetPropertyInfo().Name;
            options = _keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<label>{base.LabelText}</label>" +
                   $"<select name=\"{name}\">{options}</select>" +
                   $"</div>";
        }


        private void SetDefaultOptions()
        {
            _keyValuePair.Add(new KeyValuePair<int, string>(1, "是"));
            _keyValuePair.Add(new KeyValuePair<int, string>(0, "否"));
        }
    }
}