namespace Core.Api.Routes
{
    public class AuthenticationRoute
    {
        public const string Auth = "/api/Authentication/Auth";
    }
}
namespace Core.Api.Routes
{
    public class DataListRoute
    {
        public const string GetUserDataList = "/api/DataList/GetUserDataList";
        public const string GetRoleDataList = "/api/DataList/GetRoleDataList";
        public const string GetMenuDataList = "/api/DataList/GetMenuDataList";
    }
}
namespace Core.Api.Routes
{
    public class IconRoute
    {
        public const string Index = "/api/Icon/Index";
        public const string Search = "/api/Icon/Search";
        public const string Create = "/api/Icon/Create";
        public const string Edit = "/api/Icon/Edit/{id}";
        public const string SaveEdit = "/api/Icon/SaveEdit";
        public const string Delete = "/api/Icon/Delete/{ids}";
        public const string Recover = "/api/Icon/Recover/{ids}";
        public const string Batch = "/api/Icon/Batch";
        public const string Import = "/api/Icon/Import";
    }
}
namespace Core.Api.Routes
{
    public class LogRoute
    {
        public const string Index = "/api/Log/Index";
        public const string Search = "/api/Log/Search";
        public const string Clear = "/api/Log/Clear";
    }
}
namespace Core.Api.Routes
{
    public class MenuRoute
    {
        public const string Index = "/api/Menu/Index";
        public const string Search = "/api/Menu/Search";
        public const string FindById = "/api/Menu/FindById/{id}";
        public const string Create = "/api/Menu/Create";
        public const string Edit = "/api/Menu/Edit";
        public const string Tree = "/api/Menu/Tree/{selected?}";
        public const string Delete = "/api/Menu/Delete/{ids}";
        public const string Recover = "/api/Menu/Recover/{ids}";
        public const string Normal = "/api/Menu/Normal";
        public const string Forbidden = "/api/Menu/Forbidden";
    }
}
namespace Core.Api.Routes
{
    public class PermissionRoute
    {
        public const string Index = "/api/Permission/Index";
        public const string Search = "/api/Permission/Search";
        public const string Create = "/api/Permission/Create";
        public const string Edit = "/api/Permission/Edit/{code}";
        public const string SaveEdit = "/api/Permission/SaveEdit";
        public const string Delete = "/api/Permission/Delete/{ids}";
        public const string Recover = "/api/Permission/Recover/{ids}";
        public const string PermissionTree = "/api/v1/rbac/permission/permission_tree/{code}";
    }
}
namespace Core.Api.Routes
{
    public class RoleRoute
    {
        public const string Index = "/api/Role/Index";
        public const string FindById = "/api/Role/FindById";
        public const string Search = "/api/Role/Search";
        public const string Create = "/api/Role/Create";
        public const string Edit = "/api/Role/Edit";
        public const string Delete = "/api/Role/Delete";
        public const string Recover = "/api/Role/Recover";
        public const string Normal = "/api/Role/Normal";
        public const string Forbidden = "/api/Role/Forbidden";
        public const string AssignPermission = "/api/v1/rbac/role/assign_permission";
        public const string FindListByUserGuid = "/api/v1/rbac/role/find_list_by_user_guid/{guid}";
        public const string FindSimpleList = "/api/Role/FindSimpleList";
    }
}
namespace Core.Api.Routes
{
    public class TestRoute
    {
        public const string TestAuthentication = "/api/Test/TestAuthentication";
    }
}
namespace Core.Api.Routes
{
    public class UserRoute
    {
        public const string Index = "/api/User/Index";
        public const string Search = "/api/User/Search";
        public const string Create = "/api/User/Create";
        public const string FindById = "/api/User/FindById";
        public const string Edit = "/api/User/Edit";
        public const string Delete = "/api/User/Delete";
        public const string Recover = "/api/User/Recover";
        public const string Enable = "/api/User/Enable";
        public const string Disable = "/api/User/Disable";
    }
}
