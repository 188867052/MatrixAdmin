using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension.RouteAnalyzer;

namespace Core.Api.Routes
{
    public class Cache
    {
        public static Dictionary<string, RouteInfo> Dictionary = new Dictionary<string, RouteInfo>()
        {
            {Core.Api.Routes.AuthenticationRoute.Auth, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "username", Type = "string",  BinderType = "" },
                        new ParameterInfo() {Name = "password", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetUserDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetRoleDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.DataListRoute.GetMenuDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.IconRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Icon.IconPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Create, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Icon.IconCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Edit, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.SaveEdit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Icon.IconCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Batch, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "command", Type = "string",  BinderType = "" },
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.IconRoute.Import, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Icon.IconImportViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.LogRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.LogRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Log.LogPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.LogRoute.Clear, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.MenuRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.MenuRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.FindById, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Create, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Edit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Tree, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "selected", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Normal, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.MenuRoute.Forbidden, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.PermissionRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Permission.PermissionPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Create, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Permission.PermissionCreateViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Edit, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "code", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.SaveEdit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Permission.PermissionEditViewModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.PermissionRoute.PermissionTree, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "code", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.RoleRoute.FindById, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RolePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Create, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RoleCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Edit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RoleEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Normal, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.Forbidden, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.AssignPermission, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "payload", Type = "Core.Model.Administration.Role.RoleAssignPermissionPayload",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.FindListByUserGuid, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "guid", Type = "System.Guid",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.RoleRoute.FindSimpleList, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.TestRoute.TestAuthentication, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.UserRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Api.Routes.UserRoute.Search, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Create, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.FindById, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Edit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Enable, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
                    }
                }
            },
            {Core.Api.Routes.UserRoute.Disable, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "ids", Type = "int[]",  BinderType = "" },
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
        public static async Task<T> AuthAsync<T>(string username, string password)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Auth, username, password);
        }
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
        public static async Task<T> GetUserDataListAsync<T>(string name)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(GetUserDataList, name);
        }

        /// <summary>
        /// <see cref="Controllers.DataListController.GetRoleDataList"/>
        /// </summary>
        public const string GetRoleDataList = "/api/DataList/GetRoleDataList";
        public static async Task<T> GetRoleDataListAsync<T>(string name)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(GetRoleDataList, name);
        }

        /// <summary>
        /// <see cref="Controllers.DataListController.GetMenuDataList"/>
        /// </summary>
        public const string GetMenuDataList = "/api/DataList/GetMenuDataList";
        public static async Task<T> GetMenuDataListAsync<T>(string name)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(GetMenuDataList, name);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Search"/>
        /// </summary>
        public const string Search = "/api/Icon/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Administration.Icon.IconPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Create"/>
        /// </summary>
        public const string Create = "/api/Icon/Create";
        public static async Task<T> CreateAsync<T>(Core.Model.Administration.Icon.IconCreateViewModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Create, model);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Icon/Edit/{id}";
        public static async Task<T> EditAsync<T>(int id)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Edit, id);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/api/Icon/SaveEdit";
        public static async Task<T> SaveEditAsync<T>(Core.Model.Administration.Icon.IconCreateViewModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(SaveEdit, model);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Icon/Delete/{ids}";
        public static async Task<T> DeleteAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Delete, ids);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Icon/Recover/{ids}";
        public static async Task<T> RecoverAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Recover, ids);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Batch"/>
        /// </summary>
        public const string Batch = "/api/Icon/Batch";
        public static async Task<T> BatchAsync<T>(string command, int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Batch, command, ids);
        }

        /// <summary>
        /// <see cref="Controllers.IconController.Import"/>
        /// </summary>
        public const string Import = "/api/Icon/Import";
        public static async Task<T> ImportAsync<T>(Core.Model.Administration.Icon.IconImportViewModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Import, model);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.LogController.Search"/>
        /// </summary>
        public const string Search = "/api/Log/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Log.LogPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.LogController.Clear"/>
        /// </summary>
        public const string Clear = "/api/Log/Clear";
        public static async Task<T> ClearAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Clear);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Search"/>
        /// </summary>
        public const string Search = "/api/Menu/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Administration.Menu.MenuPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.FindById"/>
        /// </summary>
        public const string FindById = "/api/Menu/FindById/{id}";
        public static async Task<T> FindByIdAsync<T>(int id)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(FindById, id);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Create"/>
        /// </summary>
        public const string Create = "/api/Menu/Create";
        public static async Task<T> CreateAsync<T>(Core.Model.Administration.Menu.MenuCreatePostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Create, model);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Menu/Edit";
        public static async Task<T> EditAsync<T>(Core.Model.Administration.Menu.MenuEditPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Edit, model);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Tree"/>
        /// </summary>
        public const string Tree = "/api/Menu/Tree/{selected?}";
        public static async Task<T> TreeAsync<T>(string selected)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Tree, selected);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Menu/Delete/{ids}";
        public static async Task<T> DeleteAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Delete, ids);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Menu/Recover/{ids}";
        public static async Task<T> RecoverAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Recover, ids);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Normal"/>
        /// </summary>
        public const string Normal = "/api/Menu/Normal";
        public static async Task<T> NormalAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Normal, ids);
        }

        /// <summary>
        /// <see cref="Controllers.MenuController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/api/Menu/Forbidden";
        public static async Task<T> ForbiddenAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Forbidden, ids);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.Search"/>
        /// </summary>
        public const string Search = "/api/Permission/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Administration.Permission.PermissionPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.Create"/>
        /// </summary>
        public const string Create = "/api/Permission/Create";
        public static async Task<T> CreateAsync<T>(Core.Model.Administration.Permission.PermissionCreateViewModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Create, model);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Permission/Edit/{code}";
        public static async Task<T> EditAsync<T>(string code)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Edit, code);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/api/Permission/SaveEdit";
        public static async Task<T> SaveEditAsync<T>(Core.Model.Administration.Permission.PermissionEditViewModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(SaveEdit, model);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Permission/Delete/{ids}";
        public static async Task<T> DeleteAsync<T>(string ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Delete, ids);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Permission/Recover/{ids}";
        public static async Task<T> RecoverAsync<T>(string ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Recover, ids);
        }

        /// <summary>
        /// <see cref="Controllers.PermissionController.PermissionTree"/>
        /// </summary>
        public const string PermissionTree = "/api/v1/permission/permission_tree/{code}";
        public static async Task<T> PermissionTreeAsync<T>(int code)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(PermissionTree, code);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.FindById"/>
        /// </summary>
        public const string FindById = "/api/Role/FindById";
        public static async Task<T> FindByIdAsync<T>(int id)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(FindById, id);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Search"/>
        /// </summary>
        public const string Search = "/api/Role/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Administration.Role.RolePostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Create"/>
        /// </summary>
        public const string Create = "/api/Role/Create";
        public static async Task<T> CreateAsync<T>(Core.Model.Administration.Role.RoleCreatePostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Create, model);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Edit"/>
        /// </summary>
        public const string Edit = "/api/Role/Edit";
        public static async Task<T> EditAsync<T>(Core.Model.Administration.Role.RoleEditPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Edit, model);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Delete"/>
        /// </summary>
        public const string Delete = "/api/Role/Delete";
        public static async Task<T> DeleteAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Delete, ids);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Recover"/>
        /// </summary>
        public const string Recover = "/api/Role/Recover";
        public static async Task<T> RecoverAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Recover, ids);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Normal"/>
        /// </summary>
        public const string Normal = "/api/Role/Normal";
        public static async Task<T> NormalAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Normal, ids);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/api/Role/Forbidden";
        public static async Task<T> ForbiddenAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Forbidden, ids);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.AssignPermission"/>
        /// </summary>
        public const string AssignPermission = "/api/v1/role/assign_permission";
        public static async Task<T> AssignPermissionAsync<T>(Core.Model.Administration.Role.RoleAssignPermissionPayload payload)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(AssignPermission, payload);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.FindListByUserGuid"/>
        /// </summary>
        public const string FindListByUserGuid = "/api/v1/role/find_list_by_user_guid/{guid}";
        public static async Task<T> FindListByUserGuidAsync<T>(System.Guid guid)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(FindListByUserGuid, guid);
        }

        /// <summary>
        /// <see cref="Controllers.RoleController.FindSimpleList"/>
        /// </summary>
        public const string FindSimpleList = "/api/Role/FindSimpleList";
        public static async Task<T> FindSimpleListAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(FindSimpleList);
        }
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
        public static async Task<T> TestAuthenticationAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(TestAuthentication);
        }
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
        public static async Task<T> IndexAsync<T>()
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Index);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Search"/>
        /// </summary>
        public const string Search = "/api/User/Search";
        public static async Task<T> SearchAsync<T>(Core.Model.Administration.User.UserPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Search, model);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Create"/>
        /// </summary>
        public const string Create = "/api/User/Create";
        public static async Task<T> CreateAsync<T>(Core.Model.Administration.User.UserCreatePostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Create, model);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.FindById"/>
        /// </summary>
        public const string FindById = "/api/User/FindById";
        public static async Task<T> FindByIdAsync<T>(int id)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(FindById, id);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Edit"/>
        /// </summary>
        public const string Edit = "/api/User/Edit";
        public static async Task<T> EditAsync<T>(Core.Model.Administration.User.UserEditPostModel model)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Edit, model);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Delete"/>
        /// </summary>
        public const string Delete = "/api/User/Delete";
        public static async Task<T> DeleteAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Delete, ids);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Recover"/>
        /// </summary>
        public const string Recover = "/api/User/Recover";
        public static async Task<T> RecoverAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Recover, ids);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Enable"/>
        /// </summary>
        public const string Enable = "/api/User/Enable";
        public static async Task<T> EnableAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Enable, ids);
        }

        /// <summary>
        /// <see cref="Controllers.UserController.Disable"/>
        /// </summary>
        public const string Disable = "/api/User/Disable";
        public static async Task<T> DisableAsync<T>(int[] ids)
        {
            return await Core.Api.Framework.HttpClientAsync.Async2<T>(Disable, ids);
        }
    }
}
