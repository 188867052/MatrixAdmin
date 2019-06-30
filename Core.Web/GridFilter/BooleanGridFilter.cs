using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension;
using Microsoft.AspNetCore.Razor.TagHelpers;

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

        public void AddOption(bool key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<bool, string>(key, value));
        }

        public override TagHelperOutput Render()
        {
            if (this._keyValuePair.Count == 0)
            {
                this.SetDefaultOptions();
            }

            string options = this._isContainsEmpty ? "<option></option>" : default;
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            var div = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", this.ContainerClass }, });
            var divGroup = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "form-group" }, });
            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };
            var label = HtmlContentUtilities.MakeTagHelperOutput("label", labelAttributes);

            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "style", "width:204.16px" },
                { "name", this.InputName },
            };
            var select = HtmlContentUtilities.MakeTagHelperOutput("select", inputAttributes);

            select.Content.SetHtmlContent(options);
            div.Content.SetHtmlContent(divGroup);
            divGroup.Content.SetHtmlContent(label);
            label.Content.SetContent(this.LabelText);
            label.PostElement.AppendHtml(select);
            return div;
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