using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class AddRoleDialogConfiguration : DialogConfiguration<RoleCreateModel, RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleDialogConfiguration"/> class.
        /// </summary>
        public AddRoleDialogConfiguration() : base(null, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "添加用户";

        protected override void CreateBody(IList<ITextRender<RoleCreateModel, RoleModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<RoleCreateModel, RoleModel>("角色名", o => o.Name));
        }

        protected override void CreateHiddenValues(IList<ITextRender<RoleCreateModel, RoleModel>> textBoxes)
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