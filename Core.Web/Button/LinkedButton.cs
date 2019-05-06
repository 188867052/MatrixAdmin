namespace Core.Web.Button
{
    public class LinkedButton
    {
        private readonly string _class;
        private readonly string _labelText;
        private readonly string _method;

        public LinkedButton(string labelText, string method, string @class)
        {
            this._class = @class;
            this._labelText = labelText;
            this._method = method;
        }

        public string Render()
        {
            return $"<a class=\"{this._class}\" data-method=\"{this._method}\" href=\"#\">&nbsp;{this._labelText}</a>";
        }
    }
}