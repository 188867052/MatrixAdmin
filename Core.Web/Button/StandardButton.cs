using System;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.Button
{
    public class StandardButton
    {
        private readonly Identifier id;
        public StandardButton(string labelText, Identifier id = default, string @event = default)
        {
            this.Text = labelText;
            this.id = id;
            this.Event = new JavaScriptEvent(@event, id);
        }

        public string Text { get; set; }

        public JavaScriptEvent Event { get; set; }

        public string Render()
        {
            string idAttribute = this.id == default ? "" : $"id=\"{this.id.Value}\"";
            return $"<button {idAttribute} type=\"submit\" class=\"btn btn-primary\">{Text}</button>" + Environment.NewLine;
        }
    }
}
