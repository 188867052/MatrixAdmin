using System.Collections.Generic;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.User.UserIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserViewConfiguration<T> : GridConfiguration<T>
         where T : UserModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewConfiguration{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public UserViewConfiguration(HttpResponseModel entity) : base(entity)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns)
        {
            gridColumns.Add(new RowContextMenuColumn<T>(o => o.Id, "操作", UserRoute.RowContextMenu));
            gridColumns.Add(new TextGridColumn<T>(o => o.LoginName, Resources.LoginName));
            gridColumns.Add(new TextGridColumn<T>(o => o.DisplayName, Resources.DisplayName));
            gridColumns.Add(new TextGridColumn<T>(o => o.RoleName, "角色"));
            gridColumns.Add(new BooleanGridColumn<T>(o => o.IsEnable, Resources.Status));
            BooleanGridColumn<T> column = new BooleanGridColumn<T>(o => o.IsDeleted, "是否删除");
            column.AddOption(false, "正常");
            column.AddOption(true, "已删除");
            gridColumns.Add(column);
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.CreateTime, Resources.CreatedOn));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.UpdateTime, "更新时间"));
            gridColumns.Add(new TextGridColumn<T>(o => o.CreatedByUserName, Resources.CreatedByUserName));
        }
    }
}
