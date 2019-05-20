using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class EditMenuDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
        where TPostModel : MenuEditPostModel
        where TModel : MenuModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMenuDialogConfiguration"/> class.
        /// </summary>
        /// <param name="menu">The Menu.</param>
        public EditMenuDialogConfiguration(TModel menu) : base(menu, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => EditMenuDialogConfigurationResource.EditMenuTitle;

        protected override void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new HiddenTextBox<TPostModel, TModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(EditMenuDialogConfigurationResource.Name, o => o.Name, o => o.Name));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(EditMenuDialogConfigurationResource.Url, o => o.Url, o => o.Url));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(EditMenuDialogConfigurationResource.Description, o => o.Description, o => o.Description));
            textBoxes.Add(new LabeledIntegerTextBox<TPostModel, TModel>(EditMenuDialogConfigurationResource.Sort, o => o.Sort, o => o.Sort));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(EditMenuDialogConfigurationResource.Alias, o => o.Alias, o => o.Alias));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveEditUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.SaveEdit));

            buttons.Add(new StandardButton(EditMenuDialogConfigurationResource.Submit, "index.submit", saveEditUrl));
            buttons.Add(new StandardButton(EditMenuDialogConfigurationResource.Cancel, "core.cancel"));
        }
    }
}