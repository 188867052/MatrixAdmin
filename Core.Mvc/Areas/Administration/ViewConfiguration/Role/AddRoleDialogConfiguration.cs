using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class AddRoleDialogConfiguration : DialogConfiguration<RoleCreatePostModel, RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleDialogConfiguration"/> class.
        /// </summary>
        public AddRoleDialogConfiguration() : base(null, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => RoleIndexResource.AddRoleDialogTitle;

        protected override void CreateBody(IList<ITextRender<RoleCreatePostModel, RoleModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<RoleCreatePostModel, RoleModel>("角色名", o => o.Name));
            textBoxes.Add(new LabeledTextBox<RoleCreatePostModel, RoleModel>("描述", o => o.Description));
        }

        protected override void CreateHiddenValues(IList<ITextRender<RoleCreatePostModel, RoleModel>> textBoxes)
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