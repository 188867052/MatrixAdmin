using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
        /// <param name="tooltip">The tooltip.</param>
        public DropDownGridFilter(Expression<Func<T, TEnumType>> expression, string labelText, bool isContainsEmpty = true, string tooltip = default) : base(labelText, expression.GetPropertyName(), tooltip: tooltip)
        {
            this._isContainsEmpty = isContainsEmpty;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(TEnumType key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public override TagHelperOutput Render()
        {
            string options = this._isContainsEmpty ? "<option></option>" : default;
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            var label = HtmlContent.TagHelper("label");
            var select = HtmlContent.TagHelper("select", new TagHelperAttributeList { { "class", "form-control" }, { "style", "width:204.16px" }, { "name", this.InputName }, });
            select.Content.AppendHtml(options);
            label.Content.SetContent(this.LabelText);
            label.PostElement.AppendHtml(select);
            var divGroup = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "form-group" }, });
            divGroup.Content.AppendHtml(label);
            var div = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", this.ContainerClass }, });
            div.Content.AppendHtml(divGroup);
            return div;
        }
    }
}