using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserViewConfiguration : GridConfiguration<Model.Administration.User.User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewConfiguration"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public UserViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Model.Administration.User.User>> gridColumns)
        {
            Url url = new Url(nameof(Administration), typeof(UserController), nameof(UserController.RowContextMenu));
            gridColumns.Add(new RowContextMenuColumn<Model.Administration.User.User>(o => o.Id.ToString(), "操作", url));
            gridColumns.Add(new TextGridColumn<Model.Administration.User.User>(o => o.LoginName, UserIndexResource.LoginName));
            gridColumns.Add(new TextGridColumn<Model.Administration.User.User>(o => o.DisplayName, UserIndexResource.DisplayName));
            gridColumns.Add(new EnumGridColumn<Model.Administration.User.User>(o => o.UserType, "角色"));
            gridColumns.Add(new TextGridColumn<Model.Administration.User.User>(o => o.UserStatus.Name, UserIndexResource.Status));
            BooleanGridColumn<Model.Administration.User.User> column = new BooleanGridColumn<Model.Administration.User.User>(o => o.IsDeleted, "是否已删除");
            column.AddOption(false, "正常");
            column.AddOption(true, "已删除");
            gridColumns.Add(column);
            gridColumns.Add(new DateTimeGridColumn<Model.Administration.User.User>(o => o.CreateTime, UserIndexResource.CreatedOn));
            gridColumns.Add(new DateTimeGridColumn<Model.Administration.User.User>(o => o.UpdateTime, "更新时间"));
            gridColumns.Add(new TextGridColumn<Model.Administration.User.User>(o => o.CreatedByUserName, UserIndexResource.CreatedByUserName));
        }
    }
}
