using Core.Shared.Utilities;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        private readonly string _inputType;

        protected BaseGridFilter(string labelText, string inputName, TextBoxTypeEnum type = default, string tooltip = default)
        {
            Check.NotEmpty(labelText, nameof(labelText));
            Check.NotEmpty(inputName, nameof(inputName));

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
            TagHelperAttributeList labelAttributes = new TagHelperAttributeList
            {
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title", this.Tooltip },
            };

            TagHelperAttributeList inputAttributes = new TagHelperAttributeList
            {
                { "class", "form-control" },
                { "id", id },
                { "name", this.InputName },
                { "type", this._inputType },
            };

            var label = HtmlContent.TagHelper("label", labelAttributes, this.LabelText);
            var input = HtmlContent.TagHelper("input", inputAttributes);
            var divGroup = HtmlContent.TagHelper("div", new TagHelperAttribute("class", "form-group"), label, input);
            return HtmlContent.TagHelper("div", new TagHelperAttribute("class", this.ContainerClass), divGroup);
        }
    }
}