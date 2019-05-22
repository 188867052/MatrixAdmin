using Core.Web.Enums;
using Core.Web.Identifiers;

namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        private readonly string _inputType;

        protected BaseGridFilter(string labelText, string inputName, TextBoxTypeEnum type = default, string tooltip = default)
        {
            this.Tooltip = tooltip == default ? string.Empty : $"data-toggle=\"tooltip\" data-placement=\"top\" title=\"{tooltip}\"";
            this.LabelText = labelText;
            this.InputName = inputName;
            this._inputType = JavaScriptEnumMappings.ToString(type);
        }

        protected string ContainerClass { get; } = "custom-control-inline";

        protected string Tooltip { get; set; }

        protected string InputName { get; set; }

        protected string LabelText { get; }

        public virtual string Render()
        {
            string id = new Identifier().Value;

            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label for=\"{id}\" {this.Tooltip}>{this.LabelText}</label>" +
                   $"<input class=\"form-control\" id=\"{id}\" name=\"{this.InputName}\" type=\"{this._inputType}\">" +
                   $"</div>" +
                   $"</div>";
        }
    }
}