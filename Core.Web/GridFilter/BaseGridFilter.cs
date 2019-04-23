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
            return $"<div class=\"form-group\"><input class=\"form-control\" type=\"text\" value=\"{Value}\" placeholder=\"{this.Value}\"></div>";
        }
    }
}