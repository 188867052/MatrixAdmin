using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Web.Button;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Web.ViewConfiguration
{
    public abstract class DialogConfiguration<T> : IRender
    {
        public abstract string Title { get; }

        public Identifier Identifier { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected DialogConfiguration(ResponseModel model, Identifier id)
        {
            this.Identifier = id;
        }

        private string Buttons
        {
            get
            {
                IList<StandardButton> buttons = new List<StandardButton>();
                this.CreateButtons(buttons);
                return buttons.Aggregate<StandardButton, string>(default, (current, item) => current + item.Render());
            }
        }

        private string Body
        {
            get
            {
                IList<LabeledTextBox<T>> textBoxes = new List<LabeledTextBox<T>>();
                this.CreateBody(textBoxes);
                return textBoxes.Aggregate<LabeledTextBox<T>, string>(default, (current, item) => current + item.Render());
            }
        }

        protected abstract void CreateButtons(IList<StandardButton> buttons);

        protected abstract void CreateBody(IList<LabeledTextBox<T>> textBoxes);

        public virtual string Render()
        {
            string html = System.IO.File.ReadAllText("C:\\Users\\54215\\Desktop\\Study\\Asp.Net\\Core.Mvc\\wwwroot\\html\\LargeDialog.html");
            html = html.Replace("{{id}}", Identifier.Value);
            html = html.Replace("{{modal-title}}", Title);
            html = html.Replace("{{modal-body}}", this.Body);
            html = html.Replace("{{modal-footer}}", Buttons);
            return html;
        }
    }
}
