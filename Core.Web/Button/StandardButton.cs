using System;

namespace Core.Web.Button
{
    public class StandardButton
    {
        public StandardButton(string text, string @event)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public string Render()
        {
            return $"<button type=\"submit\" class=\"btn btn-primary\">{Text}</button>" + Environment.NewLine;
        }
    }
}
