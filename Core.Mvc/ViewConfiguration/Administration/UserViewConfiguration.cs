using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class UserViewConfiguration : GridConfiguration<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public UserViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.LoginName, UserIndexResource.LoginName));
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.DisplayName, UserIndexResource.DisplayName));
            GridColumn.AddEnumColumn(new EnumGridColumn<User>(o => o.UserType, UserIndexResource.UserType));
            GridColumn.AddEnumColumn(new EnumGridColumn<User>(o => o.Status, UserIndexResource.Status));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<User>(o => o.CreatedOn, UserIndexResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.CreatedByUserName, UserIndexResource.CreatedByUserName));
        }
    }
}
