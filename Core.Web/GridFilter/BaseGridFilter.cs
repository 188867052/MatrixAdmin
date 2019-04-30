using System.Reflection;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        public readonly string ContainerClass = "custom-control-inline";
        private readonly string type;

        protected BaseGridFilter(string labelText, PropertyInfo propertyInfo = default, TextBoxTypeEnum type = default)
        {
            this.LabelText = labelText;
            this.PropertyInfo = propertyInfo;
            this.type = type.ToString();
        }

        public PropertyInfo PropertyInfo { get; set; }

        public string LabelText { get; }

        public virtual string Render()
        {
            string id = new Identifier().Value;
            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label for=\"{id}\">{this.LabelText}</label>" +
                   $"<input class=\"form-control\" id=\"{id}\" type=\"{type}\">" +
                   $"</div>" +
                   $"</div>";
        }
    }
}