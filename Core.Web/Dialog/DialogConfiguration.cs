using System.Collections.Generic;
using System.Linq;
using Core.Entity;
using Core.Web.Button;
using Core.Web.Html;
using Core.Web.Identifiers;

namespace Core.Web.Dialog
{
    public abstract class DialogConfiguration<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogConfiguration{TPostModel, TModel}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The id.</param>
        protected DialogConfiguration(Identifier id, TModel model = default)
        {
            this.Model = model;
            this.Identifier = id;
        }

        public TModel Model { get; }

        public abstract string Title { get; }

        public Identifier Identifier { get; }

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
                IList<ITextRender<TPostModel, TModel>> textBoxes = new List<ITextRender<TPostModel, TModel>>();
                this.CreateBody(textBoxes);
                this.CreateHiddenValues(textBoxes);
                return textBoxes.Aggregate<ITextRender<TPostModel, TModel>, string>(default, (current, item) => current + item.Render(this.Model));
            }
        }

        public virtual string Render(TModel model)
        {
            string html = new CoreContext().Configuration.FirstOrDefault(o => o.Key == "Dialog").Value;
            html = html.Replace("{{id}}", this.Identifier.Value);
            html = html.Replace("{{modal-title}}", this.Title);
            html = html.Replace("{{modal-body}}", this.Body);
            html = html.Replace("{{modal-footer}}", this.Buttons);
            return html;
        }

        protected abstract void CreateButtons(IList<StandardButton> buttons);

        protected abstract void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes);

        protected abstract void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes);
    }
}
