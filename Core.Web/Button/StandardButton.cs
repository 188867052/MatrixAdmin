using System;
using Core.Web.GridFilter;
using Core.Web.Identifiers;
using Core.Web.JavaScript;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                 { "class", "btn btn-primary" },
                 { "id", this._id.Value },
                 { "data-url", this._url },
                 { "data-toggle", "tooltip" },
                 { "data-placement", "top" },
                 { "title",  this._tooltip },
                 { "type", this._tooltip },
            };

            var output = HtmlContentUtilities.MakeTagHelperOutput("buttons", attributes);
            output.Content.SetContent(this.Text);
            var button = HtmlContentUtilities.HtmlContentToString(output);

            return button + Environment.NewLine + this.Event.Render();
        }
    }
}
