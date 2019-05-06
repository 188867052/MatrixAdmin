namespace Core.Web.RowContextMenu
{
    public class RowContextMenuLink
    {
        private readonly string labelText;
        private readonly string method;

        public RowContextMenuLink(string labelText, string method)
        {
            this.labelText = labelText;
            this.method = method;
        }

        public string Render()
        {
            return $"<a class=\"icon-edit dropdown-item\" data-method=\"{this.method}\" href=\"#\">&nbsp;{this.labelText}</a>";
        }
    }
}