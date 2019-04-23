namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        protected BaseGridFilter(string value)
        {
            this.Value = value;
        }

        private string Value { get; }

        public virtual string Render()
        {
            string a = $"<div class=\"custom-control-inline\">" +
                       $"<label class=\"custom-control-label\" for=\"customRadio1\">{this.Value}</label>" +
                       $"<input class=\"form-control\" type=\"text\" value=\"{this.Value}\" placeholder=\"{this.Value}\">" +
                       $"</div>";
            return a;
            //return $"<div class=\"form-group\"><input class=\"form-control\" type=\"text\" value=\"{Value}\" placeholder=\"{this.Value}\"></div>";
        }
    }
}