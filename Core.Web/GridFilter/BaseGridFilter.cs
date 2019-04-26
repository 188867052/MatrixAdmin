namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        protected BaseGridFilter(string value)
        {
            this.LabelText = value;
        }

        public string LabelText { get; }

        public string Class = "custom-control-inline";

        public virtual string Render()
        {
            return $"<div class=\"{Class}\">" +
                      $"<label>{this.LabelText}</label>" +
                      $"<input type=\"text\">" +
                   $"</div>";
        }
    }
}