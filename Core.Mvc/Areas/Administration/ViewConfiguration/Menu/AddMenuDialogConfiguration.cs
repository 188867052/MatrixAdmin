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

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class AddMenuDialogConfiguration : DialogConfiguration<MenuCreatePostModel, MenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMenuDialogConfiguration"/> class.
        /// </summary>
        public AddMenuDialogConfiguration() : base(null, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "添加用户";

        protected override void CreateBody(IList<ITextRender<MenuCreatePostModel, MenuModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<MenuCreatePostModel, MenuModel>("菜单名", o => o.Name));
            textBoxes.Add(new LabeledTextBox<MenuCreatePostModel, MenuModel>("链接地址", o => o.Url));
            textBoxes.Add(new LabeledTextBox<MenuCreatePostModel, MenuModel>("描述信息", o => o.Description));
            textBoxes.Add(new LabeledIntegerTextBox<MenuCreatePostModel, MenuModel>("排序", o => o.Sort));
            textBoxes.Add(new LabeledTextBox<MenuCreatePostModel, MenuModel>("页面别名", o => o.Alias));
        }

        protected override void CreateHiddenValues(IList<ITextRender<MenuCreatePostModel, MenuModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveCreateUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.SaveCreate));

            buttons.Add(new StandardButton("提交", "index.submit", saveCreateUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}