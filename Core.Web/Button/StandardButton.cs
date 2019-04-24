using System;
using Core.Web.JavaScript;

namespace Core.Web.Button
{
    public class StandardButton
    {
        public StandardButton(string text, string @event = default)
        {
            this.Text = text;

            this.Delegate = "alert(this.value)";

            this.Event = new JavaScriptEvent(Delegate);
        }

        public string Text { get; set; }

        public string Delegate { get; set; }

        public JavaScriptEvent Event { get; set; }

        public string Render()
        {
            return $"<button type=\"submit\" class=\"btn btn-primary\">{Text}</button>" + Environment.NewLine;
        }
    }
}
