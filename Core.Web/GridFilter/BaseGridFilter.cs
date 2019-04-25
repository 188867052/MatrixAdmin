namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        protected BaseGridFilter(string value)
        {
            this.LabelText = value;
        }

        public string LabelText { get; }

        public virtual string Render()
        {
            return $"<div class=\"custom-control-inline\">" +
                      $"<label>{this.LabelText}</label>" +
                      $"<input type=\"text\">" +
                   $"</div>";
        }
    }
}