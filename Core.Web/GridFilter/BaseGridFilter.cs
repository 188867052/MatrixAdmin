namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter
    {
        protected BaseGridFilter(string value)
        {
            this.Value = value;
        }

        public string Value { get; }

        public virtual string Render()
        {
            return $"<div class=\"custom-control-inline\">" +
                      $"<label>{this.Value}</label>" +
                      $"<input type=\"text\">" +
                   $"</div>";
        }
    }
}