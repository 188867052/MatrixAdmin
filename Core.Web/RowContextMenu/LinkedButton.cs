namespace Core.Web.RowContextMenu
{
    public class LinkedButton
    {
        private readonly string _class;
        private readonly string _labelText;
        private readonly string _method;

        public LinkedButton(string labelText, string method, string @class)
        {
            _class = @class;
            _labelText = labelText;
            _method = method;
        }
        public string Render()
        {
            return $"<a class=\"{_class}\" data-method=\"{_method}\" href=\"#\">&nbsp;{_labelText}</a>";
        }
    }
}