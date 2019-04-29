﻿using Core.Model;
using Core.Model.Administration.Role;
using Core.Resource;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class RoleViewConfiguration : GridConfiguration<Role>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public RoleViewConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<Role>(o => o.Name, RoleIndexResource.Name));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.Status, RoleIndexResource.Status));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.IsBuiltin, RoleIndexResource.IsBuiltin));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Role>(o => o.IsSuperAdministrator, RoleIndexResource.IsSuperAdministrator));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Role>(o => o.CreatedOn, RoleIndexResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<Role>(o => o.CreatedByUserName, RoleIndexResource.CreatedByUserName));
        }
    }
}