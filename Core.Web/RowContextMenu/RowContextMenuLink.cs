namespace Core.Web.RowContextMenu
{
    public class RowContextMenuLink
    {
        public string labelText;
        public string method;
        public RowContextMenuLink(string labelText, string method)
        {
            this.labelText = labelText;
            this.method = method;
        }

        public string Render()
        {
            return $"<a class=\"icon-edit dropdown-item\" data-method=\"{method}\" href=\"#\">&nbsp;{labelText}</a>";
        }
    }
}