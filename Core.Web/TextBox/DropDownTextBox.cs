using System;
using System.Collections.Generic;
using System.Linq;
using Core.Web.Html;

namespace Core.Web.TextBox
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="TPostModel">The post model.</typeparam>
    /// <typeparam name="TEnumType">The enum.</typeparam>
    public class DropDownTextBox<TPostModel, TEnumType> : ITextRender<TPostModel, TEnumType>
    {
        private readonly IList<KeyValuePair<int, string>> _keyValuePair;
        private readonly bool _isContainsEmpty;
        private string inputName = "InputName";
        private string labelText;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownTextBox{TPostModel, TEnumType}"/> class.
        /// </summary>
        /// <param name="labelText">The labelText.</param>
        /// <param name="isContainsEmpty">The isContainsEmpty.</param>
        public DropDownTextBox(string labelText, bool isContainsEmpty = true)
        {
            this._isContainsEmpty = isContainsEmpty;
            this.labelText = labelText;
            this._keyValuePair = new List<KeyValuePair<int, string>>();
        }

        public void AddOption(TEnumType key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>((int)Enum.Parse(key.GetType(), key.ToString()), value));
        }

        public void AddOption(int key, string value)
        {
            this._keyValuePair.Add(new KeyValuePair<int, string>(key, value));
        }

        public string Render(TEnumType model)
        {
            string options = this._isContainsEmpty ? "<option></option>" : default;
            options = this._keyValuePair.Aggregate(options, (current, item) => current + $"<option value='{item.Key}'>{item.Value}</option>");

            return
                $"<div class=\"form-group\">" +
                $"<label>{this.labelText}</label>" +
                $"<select class=\"form-control\" name=\"{this.inputName}\">" +
                $"{options}" +
                $"</select>" +
                $"</div>";
        }
    }
}