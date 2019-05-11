using System;
using Core.Extension;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.Button
{
    public class StandardButton
    {
        private readonly Url _url;
        private readonly Identifier _id;

        public StandardButton(string labelText, string @event = default, Url url = null)
        {
            this._url = url;
            this.Text = labelText;
            this._id = new Identifier();
            this.Event = new JavaScriptEvent(@event, this._id);
        }

        public string Text { get; set; }

        public JavaScriptEvent Event { get; set; }

        public string Render()
        {
            string idAttribute = this._id == default ? string.Empty : $"id=\"{this._id.Value}\"";
            string dataUrl = this._url == null ? string.Empty : $"data-url=\"{this._url.Render()}\"";
            return $"<button {idAttribute} type=\"submit\" class=\"btn btn-primary\" {dataUrl}>{this.Text}</button>" + Environment.NewLine + this.Event.Render();
        }
    }
}
