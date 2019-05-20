using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class AddRoleDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
        where TPostModel : RoleCreatePostModel
        where TModel : RoleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleDialogConfiguration{TPostModel, TModel}"/> class.
        /// </summary>
        public AddRoleDialogConfiguration() : base(RoleIdentifiers.AddRoleDialogIdentifier)
        {
        }

        public override string Title => RoleIndexResource.AddRoleDialogTitle;

        protected override void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("角色名", o => o.Name));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("描述", o => o.Description));
        }

        protected override void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveCreateUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.SaveCreate));

            buttons.Add(new StandardButton("提交", "index.submit", saveCreateUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}