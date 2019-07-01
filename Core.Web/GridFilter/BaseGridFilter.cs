using Core.Web.Enums;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        private readonly string _inputType;

        protected BaseGridFilter(string labelText, string inputName, TextBoxTypeEnum type = default, string tooltip = default)
        {
            this.Tooltip = tooltip;
            this.LabelText = labelText;
            this.InputName = inputName;
            this._inputType = JavaScriptEnumMappings.ToString(type);
        }

        protected string ContainerClass { get; } = "custom-control-inline";

        protected string Tooltip { get; set; }

        protected string InputName { get; set; }

        protected string LabelText { get; }

        public virtual TagHelperOutput Render()
        {
            string id = new Identifier().Value;
            var div = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", this.ContainerClass }, });
            var divGroup = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "form-group" }, });
            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };
            var label = HtmlContent.TagHelper("label", labelAttributes);

            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "id", id },
                { "name", this.InputName },
                { "type", this._inputType },
            };
            var input = HtmlContent.TagHelper("input", inputAttributes);

            div.Content.SetHtmlContent(divGroup);
            divGroup.Content.SetHtmlContent(label);
            label.Content.SetContent(this.LabelText);
            label.PostElement.AppendHtml(input);
            return div;
        }
    }
}