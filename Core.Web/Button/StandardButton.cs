using System;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.Button
{
    public class StandardButton
    {
        private readonly Identifier id;

        public StandardButton(string labelText, string @event = default)
        {
            this.Text = labelText;
            this.id = new Identifier();
            this.Event = new JavaScriptEvent(@event, this.id);
        }

        public string Text { get; set; }

        public JavaScriptEvent Event { get; set; }

        public string Render()
        {
            string idAttribute = this.id == default ? string.Empty : $"id=\"{this.id.Value}\"";
            return $"<button {idAttribute} type=\"submit\" class=\"btn btn-primary\">{this.Text}</button>" + Environment.NewLine + this.Event.Render();
        }
    }
}
