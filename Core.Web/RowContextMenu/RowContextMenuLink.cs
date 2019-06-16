using Core.Extension;

namespace Core.Web.RowContextMenu
{
    public class RowContextMenuLink
    {
        private readonly string _labelText;
        private readonly string _method;
        private readonly string _url;

        public RowContextMenuLink(string labelText, string method, string url)
        {
            this._labelText = labelText;
            this._method = method;
            this._url = url;
        }

        public string Render()
        {
            return $"<a class=\"icon-edit dropdown-item\" data-url=\"{this._url}\" data-method=\"{this._method}\" href=\"#\">&nbsp;{this._labelText}</a>";
        }
    }
}