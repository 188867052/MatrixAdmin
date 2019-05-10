using Core.Extension;

namespace Core.Web.RowContextMenu
{
    public class RowContextMenuLink
    {
        private readonly string _labelText;
        private readonly string _method;
        private readonly Url _url;

        public RowContextMenuLink(string labelText, string method, Url url)
        {
            this._labelText = labelText;
            this._method = method;
            this._url = url;
        }

        public string Render()
        {
            return $"<a class=\"icon-edit dropdown-item\" data-url=\"{this._url.Render()}\" data-method=\"{this._method}\" href=\"#\">&nbsp;{this._labelText}</a>";
        }
    }
}