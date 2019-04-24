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
            return $"<div class=\"custom-control-inline\">" +
                       $"<label class=\"custom-control-label\">{this.Value}</label>" +
                       $"<input class=\"form-control\" type=\"text\">" +
                       $"</div>";
        }
    }
}