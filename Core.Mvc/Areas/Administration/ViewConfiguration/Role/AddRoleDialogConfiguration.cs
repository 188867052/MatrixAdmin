using System.Collections.Generic;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Routes;
using Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Role;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
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

        protected override void CreateBody(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("角色名", o => o.Name));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>("描述", o => o.Description));
        }

        protected override void CreateHiddenValues(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", "index.submit", RoleRoute.SaveCreate));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}