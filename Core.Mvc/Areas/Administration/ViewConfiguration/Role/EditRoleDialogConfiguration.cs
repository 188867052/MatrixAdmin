using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration.Role;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class EditRoleDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
        where TPostModel : RoleEditPostModel
        where TModel : RoleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditRoleDialogConfiguration{TPostModel, TModel}"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EditRoleDialogConfiguration(TModel user) : base(RoleIdentifiers.EditRoleDialogIdentifier, user)
        {
        }

        public override string Title => RoleIndexResource.EditRoleDialogTitle;

        protected override void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new HiddenValue<TPostModel, TModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("角色名", o => o.Name, o => o.Name));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("描述", o => o.Description, o => o.Description));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveEditUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.SaveEdit));

            buttons.Add(new StandardButton("提交", "index.submit", saveEditUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}