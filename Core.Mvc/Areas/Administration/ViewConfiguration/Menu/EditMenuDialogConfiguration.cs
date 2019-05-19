using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class EditMenuDialogConfiguration : DialogConfiguration<MenuEditPostModel, MenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMenuDialogConfiguration"/> class.
        /// </summary>
        /// <param name="menu">The Menu.</param>
        public EditMenuDialogConfiguration(MenuModel menu) : base(menu, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "编辑菜单";

        protected override void CreateHiddenValues(IList<ITextRender<MenuEditPostModel, MenuModel>> textBoxes)
        {
            textBoxes.Add(new HiddenTextBox<MenuEditPostModel, MenuModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<MenuEditPostModel, MenuModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<MenuEditPostModel, MenuModel>("菜单名", o => o.Name, o => o.Name));
            textBoxes.Add(new LabeledTextBox<MenuEditPostModel, MenuModel>("链接地址", o => o.Url, o => o.Url));
            textBoxes.Add(new LabeledTextBox<MenuEditPostModel, MenuModel>("描述信息", o => o.Description, o => o.Description));
            textBoxes.Add(new LabeledIntegerTextBox<MenuEditPostModel, MenuModel>("排序", o => o.Sort, o => o.Sort));
            textBoxes.Add(new LabeledTextBox<MenuEditPostModel, MenuModel>("页面别名", o => o.Alias, o => o.Alias));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveEditUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.SaveEdit));

            buttons.Add(new StandardButton("提交", "index.submit", saveEditUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}