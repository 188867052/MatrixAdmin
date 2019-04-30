using System.Reflection;

namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        public readonly string ContainerClass = "custom-control-inline";

        protected BaseGridFilter(string labelText, PropertyInfo propertyInfo = default, string inputClass = default)
        {
            this.LabelText = labelText;
            this.InputClass = inputClass;
            this.PropertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo { get; set; }

        public string InputClass { get; set; }

        public string LabelText { get; }

        public virtual string Render()
        {
            string inputClass = default; /*this.InputClass == default ? default : $"class=\"{this.InputClass}\"";*/
            //return $"<div class=\"{this.ContainerClass}\">" +
            //       "<div class=\"form-group\">"+
            //          $"<label>{this.LabelText}</label>" +
            //          $"<input name=\"{this.PropertyInfo.Name}\" type=\"text\" {inputClass}>" +
            //       $"</div>"+
            //       $"</div>";


            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label for=\"email\">{this.LabelText}</label>" +
                   $"<input {inputClass} type = \"email\" class=\"form-control\" id=\"email\">" +
                   $"</div>" +
                   $"</div>";
        }
    }
}