using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Web.Button;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Web.ViewConfiguration
{
    public abstract class DialogConfiguration<TPostModel, T> : ITextRender<TPostModel, T>
    {
        private T model;
        public abstract string Title { get; }

        public Identifier Identifier { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected DialogConfiguration(T model, Identifier id)
        {
            this.model = model;
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
                IList<ITextRender<TPostModel, T>> textBoxes = new List<ITextRender<TPostModel, T>>();
                this.CreateBody(textBoxes);
                return textBoxes.Aggregate<ITextRender<TPostModel, T>, string>(default, (current, item) => current + item.Render(this.model));
            }
        }

        protected abstract void CreateButtons(IList<StandardButton> buttons);

        protected abstract void CreateBody(IList<ITextRender<TPostModel, T>> textBoxes);

        public virtual string Render(T model)
        {
            string html = System.IO.File.ReadAllText("C:\\Users\\54215\\Desktop\\Study\\Asp.Net\\Core.Mvc\\wwwroot\\html\\dialog.html");
            html = html.Replace("{{id}}", Identifier.Value);
            html = html.Replace("{{modal-title}}", Title);
            html = html.Replace("{{modal-body}}", this.Body);
            html = html.Replace("{{modal-footer}}", Buttons);
            return html;
        }
    }
}
