using System;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.Button
{
    public class StandardButton
    {
        private readonly string _url;
        private readonly string _tooltip;
        private readonly Identifier _id;

        public StandardButton(string labelText, string @event = default, string url = null, string tooltip = default)
        {
            this._url = url;
            this._tooltip = tooltip;
            this.Text = labelText;
            this._id = new Identifier();
            this.Event = new JavaScriptEvent(func: @event, id: this._id);
        }

        public string Text { get; set; }

        public JavaScriptEvent Event { get; set; }

        public string Render()
        {
            string tooltip = this._tooltip == default ? string.Empty : $"data-toggle=\"tooltip\" data-placement=\"top\" title=\"{this._tooltip}\"";
            string idAttribute = this._id == default ? string.Empty : $"id=\"{this._id.Value}\"";
            string dataUrl = this._url == null ? string.Empty : $"data-url=\"{this._url}\"";

            return $"<button {idAttribute} type=\"submit\" class=\"btn btn-primary\" {tooltip} {dataUrl}>{this.Text}</button>" + Environment.NewLine + this.Event.Render();
        }
    }
}
