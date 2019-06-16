using System.Collections.Generic;
using Core.Extension.RouteAnalyzer;

namespace Core.Api.Routes
{
    public class Cache
    {
        public static Dictionary<string, dynamic> Dictionary = new Dictionary<string, dynamic>()
        {
            {Core.Api.Routes.AuthenticationRoute.Auth, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "username", Type = "String",  BinderType = "" },
                        new ParameterInfo() {Name = "password", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetUserDataList, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetRoleDataList, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetMenuDataList, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.IconRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "IconPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Create, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "IconCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Edit, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "Int32",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.SaveEdit, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "IconCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Delete, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Recover, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Batch, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "command", Type = "String",  BinderType = "" },
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Import, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "IconImportViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.LogRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.LogRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "LogPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.LogRoute.Clear, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.MenuRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.MenuRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "MenuPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.FindById, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "Int32",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Create, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "MenuCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Edit, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "MenuEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Tree, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "selected", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Delete, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Recover, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Normal, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Forbidden, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.PermissionRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "PermissionPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Create, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "PermissionCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Edit, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "code", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.SaveEdit, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "PermissionEditViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Delete, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Recover, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "String",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.PermissionTree, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "code", Type = "Int32",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.RoleRoute.FindById, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "Int32",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "RolePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Create, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "RoleCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Edit, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "RoleEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Delete, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Recover, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Normal, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Forbidden, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.AssignPermission, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "payload", Type = "RoleAssignPermissionPayload",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.FindListByUserGuid, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "guid", Type = "Guid",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.FindSimpleList, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.TestRoute.TestAuthentication, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.UserRoute.Index, new
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.UserRoute.Search, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "UserPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Create, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "UserCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.FindById, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "Int32",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Edit, new
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "UserEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Delete, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Recover, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Enable, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Disable, new
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "Int32[]",  BinderType = "" },
                    }
                }
            },
        };
    }

    /// <summary>
    /// <see cref="Controllers.AuthenticationController"/>
    /// </summary>
    public class AuthenticationRoute
    {
        /// <summary>
        /// <see cref="Controllers.AuthenticationController.Auth"/>
        /// </summary>
        public const string Auth = "/api/Authentication/Auth";
    }

    /// <summary>
    /// <see cref="Controllers.DataListController"/>
    /// </summary>
    public class DataListRoute
    {
        /// <summary>
        /// <see cref="Controllers.DataListController.GetUserDataList"/>
        /// </summary>
        public const string GetUserDataList = "/api/DataList/GetUserDataList";

        /// <summary>
        /// <see cref="Controllers.DataListController.GetRoleDataList"/>
        /// </summary>
        public const string GetRoleDataList = "/api/DataList/GetRoleDataList";

        /// <summary>
        /// <see cref="Controllers.DataListController.GetMenuDataList"/>
        /// </summary>
        public const string GetMenuDataList = "/api/DataList/GetMenuDataList";
    }

    /// <summary>
    /// <see cref="Controllers.IconController"/>
    /// </summary>
    public class IconRoute
    {
        /// <summary>
        /// <see cref="Controllers.IconController.Index"/>
        /// </summary>
        public const string Index = "/api/Icon/Index";

        /// <summary>
        /// <see cref="Controllers.IconController.Search"/>
        /// </summary>
        public const string Search = "/api/Icon/Search";

        /// <summary>
        /// <see cref="Controllers.IconController.Create"/>
        /// </summary>
        public const string Create = "/api/Icon/Create";

        /// <summary>
        /// <see cref="Controllers.IconController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Icon/Edit/{id}";

        /// <summary>
        /// <see cref="Controllers.IconController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/api/Icon/SaveEdit";

        /// <summary>
        /// <see cref="Controllers.IconController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Icon/Delete/{ids}";

        /// <summary>
        /// <see cref="Controllers.IconController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Icon/Recover/{ids}";

        /// <summary>
        /// <see cref="Controllers.IconController.Batch"/>
        /// </summary>
        public const string Batch = "/api/Icon/Batch";

        /// <summary>
        /// <see cref="Controllers.IconController.Import"/>
        /// </summary>
        public const string Import = "/api/Icon/Import";
    }

    /// <summary>
    /// <see cref="Controllers.LogController"/>
    /// </summary>
    public class LogRoute
    {
        /// <summary>
        /// <see cref="Controllers.LogController.Index"/>
        /// </summary>
        public const string Index = "/api/Log/Index";

        /// <summary>
        /// <see cref="Controllers.LogController.Search"/>
        /// </summary>
        public const string Search = "/api/Log/Search";

        /// <summary>
        /// <see cref="Controllers.LogController.Clear"/>
        /// </summary>
        public const string Clear = "/api/Log/Clear";
    }

    /// <summary>
    /// <see cref="Controllers.MenuController"/>
    /// </summary>
    public class MenuRoute
    {
        /// <summary>
        /// <see cref="Controllers.MenuController.Index"/>
        /// </summary>
        public const string Index = "/api/Menu/Index";

        /// <summary>
        /// <see cref="Controllers.MenuController.Search"/>
        /// </summary>
        public const string Search = "/api/Menu/Search";

        /// <summary>
        /// <see cref="Controllers.MenuController.FindById"/>
        /// </summary>
        public const string FindById = "/api/Menu/FindById/{id}";

        /// <summary>
        /// <see cref="Controllers.MenuController.Create"/>
        /// </summary>
        public const string Create = "/api/Menu/Create";

        /// <summary>
        /// <see cref="Controllers.MenuController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Menu/Edit";

        /// <summary>
        /// <see cref="Controllers.MenuController.Tree"/>
        /// </summary>
        public const string Tree = "/api/Menu/Tree/{selected?}";

        /// <summary>
        /// <see cref="Controllers.MenuController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Menu/Delete/{ids}";

        /// <summary>
        /// <see cref="Controllers.MenuController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Menu/Recover/{ids}";

        /// <summary>
        /// <see cref="Controllers.MenuController.Normal"/>
        /// </summary>
        public const string Normal = "/api/Menu/Normal";

        /// <summary>
        /// <see cref="Controllers.MenuController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/api/Menu/Forbidden";
    }

    /// <summary>
    /// <see cref="Controllers.PermissionController"/>
    /// </summary>
    public class PermissionRoute
    {
        /// <summary>
        /// <see cref="Controllers.PermissionController.Index"/>
        /// </summary>
        public const string Index = "/api/Permission/Index";

        /// <summary>
        /// <see cref="Controllers.PermissionController.Search"/>
        /// </summary>
        public const string Search = "/api/Permission/Search";

        /// <summary>
        /// <see cref="Controllers.PermissionController.Create"/>
        /// </summary>
        public const string Create = "/api/Permission/Create";

        /// <summary>
        /// <see cref="Controllers.PermissionController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Permission/Edit/{code}";

        /// <summary>
        /// <see cref="Controllers.PermissionController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/api/Permission/SaveEdit";

        /// <summary>
        /// <see cref="Controllers.PermissionController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Permission/Delete/{ids}";

        /// <summary>
        /// <see cref="Controllers.PermissionController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Permission/Recover/{ids}";

        /// <summary>
        /// <see cref="Controllers.PermissionController.PermissionTree"/>
        /// </summary>
        public const string PermissionTree = "/api/v1/permission/permission_tree/{code}";
    }

    /// <summary>
    /// <see cref="Controllers.RoleController"/>
    /// </summary>
    public class RoleRoute
    {
        /// <summary>
        /// <see cref="Controllers.RoleController.Index"/>
        /// </summary>
        public const string Index = "/api/Role/Index";

        /// <summary>
        /// <see cref="Controllers.RoleController.FindById"/>
        /// </summary>
        public const string FindById = "/api/Role/FindById";

        /// <summary>
        /// <see cref="Controllers.RoleController.Search"/>
        /// </summary>
        public const string Search = "/api/Role/Search";

        /// <summary>
        /// <see cref="Controllers.RoleController.Create"/>
        /// </summary>
        public const string Create = "/api/Role/Create";

        /// <summary>
        /// <see cref="Controllers.RoleController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Role/Edit";

        /// <summary>
        /// <see cref="Controllers.RoleController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Role/Delete";

        /// <summary>
        /// <see cref="Controllers.RoleController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Role/Recover";

        /// <summary>
        /// <see cref="Controllers.RoleController.Normal"/>
        /// </summary>
        public const string Normal = "/api/Role/Normal";

        /// <summary>
        /// <see cref="Controllers.RoleController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/api/Role/Forbidden";

        /// <summary>
        /// <see cref="Controllers.RoleController.AssignPermission"/>
        /// </summary>
        public const string AssignPermission = "/api/v1/role/assign_permission";

        /// <summary>
        /// <see cref="Controllers.RoleController.FindListByUserGuid"/>
        /// </summary>
        public const string FindListByUserGuid = "/api/v1/role/find_list_by_user_guid/{guid}";

        /// <summary>
        /// <see cref="Controllers.RoleController.FindSimpleList"/>
        /// </summary>
        public const string FindSimpleList = "/api/Role/FindSimpleList";
    }

    /// <summary>
    /// <see cref="Controllers.TestController"/>
    /// </summary>
    public class TestRoute
    {
        /// <summary>
        /// <see cref="Controllers.TestController.TestAuthentication"/>
        /// </summary>
        public const string TestAuthentication = "/api/Test/TestAuthentication";
    }

    /// <summary>
    /// <see cref="Controllers.UserController"/>
    /// </summary>
    public class UserRoute
    {
        /// <summary>
        /// <see cref="Controllers.UserController.Index"/>
        /// </summary>
        public const string Index = "/api/User/Index";

        /// <summary>
        /// <see cref="Controllers.UserController.Search"/>
        /// </summary>
        public const string Search = "/api/User/Search";

        /// <summary>
        /// <see cref="Controllers.UserController.Create"/>
        /// </summary>
        public const string Create = "/api/User/Create";

        /// <summary>
        /// <see cref="Controllers.UserController.FindById"/>
        /// </summary>
        public const string FindById = "/api/User/FindById";

        /// <summary>
        /// <see cref="Controllers.UserController.Edit"/>
        /// </summary>
        public const string Edit = "/api/User/Edit";

        /// <summary>
        /// <see cref="Controllers.UserController.Delete"/>
        /// </summary>
        public const string Delete = "/api/User/Delete";

        /// <summary>
        /// <see cref="Controllers.UserController.Recover"/>
        /// </summary>
        public const string Recover = "/api/User/Recover";

        /// <summary>
        /// <see cref="Controllers.UserController.Enable"/>
        /// </summary>
        public const string Enable = "/api/User/Enable";

        /// <summary>
        /// <see cref="Controllers.UserController.Disable"/>
        /// </summary>
        public const string Disable = "/api/User/Disable";
    }
}
