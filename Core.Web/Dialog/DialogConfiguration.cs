using System.Collections.Generic;
using System.Linq;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Html;
using Core.Web.Identifiers;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
            var modalHeader = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal-header" }, });
            var headerTitle = HtmlContentUtilities.MakeTagHelperOutput("h4", new TagHelperAttributeList { { "class", "modal-title" }, });
            headerTitle.Content.AppendHtml(this.Title);
            var headerButton = HtmlContentUtilities.MakeTagHelperOutput("button", new TagHelperAttributeList { { "type", "button" }, { "class", "close" }, { "data-dismiss", "modal" }, });
            headerButton.Content.AppendHtml("&times;");
            modalHeader.Content.AppendHtml(headerTitle);
            modalHeader.Content.AppendHtml(headerButton);

            var modalBody = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal-body" }, });
            modalBody.Content.AppendHtml(this.Body);

            var modalFooter = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal-footer" }, });
            modalFooter.Content.AppendHtml(this.Buttons);

            var modalContent = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal-content" } });
            modalContent.Content.AppendHtml(modalHeader);
            modalContent.Content.AppendHtml(modalBody);
            modalContent.Content.AppendHtml(modalFooter);

            var modalDialog = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal-dialog modal-md" }, });
            modalDialog.Content.AppendHtml(modalContent);
            var modal = HtmlContentUtilities.MakeTagHelperOutput("div", new TagHelperAttributeList { { "class", "modal fade" }, { "id", this.Identifier.Value }, });
            modal.Content.AppendHtml(modalDialog);

            var html = HtmlContentUtilities.HtmlContentToString(modal);
            return html;
        }

        protected abstract void CreateButtons(IList<StandardButton> buttons);

        protected abstract void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes);

        protected abstract void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes);
    }
}
