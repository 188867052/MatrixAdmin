using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class EditRoleDialogConfiguration : DialogConfiguration<RoleEditPostModel, RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditRoleDialogConfiguration"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EditRoleDialogConfiguration(RoleModel user) : base(user, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "编辑用户";

        protected override void CreateHiddenValues(IList<ITextRender<RoleEditPostModel, RoleModel>> textBoxes)
        {
            textBoxes.Add(new HiddenTextBox<RoleEditPostModel, RoleModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<RoleEditPostModel, RoleModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<RoleEditPostModel, RoleModel>("角色名", o => o.Name, o => o.Name));
            textBoxes.Add(new LabeledTextBox<RoleEditPostModel, RoleModel>("描述", o => o.Description, o => o.Description));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveEditUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.SaveEdit));

            buttons.Add(new StandardButton("提交", "index.submit", saveEditUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}