using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension.RouteAnalyzer;

namespace Core.Mvc.Areas.Redirect.Routes
{
    public class Cache
    {
        public static Dictionary<string, RouteInfo> Dictionary = new Dictionary<string, RouteInfo>()
        {
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Index2, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Login, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Widgets, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Buttons, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Calendar, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Charts, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Chat, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Error, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "number", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Gallery, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Interface, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Redirect.Routes.RedirectRoute.Invoice, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Log.Routes.LogRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Log.Routes.LogRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Log.LogPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Log.Routes.LogRoute.Clear, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.AdvancedDropDownFilters.AdvancedDropDownRoute.RoleDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.AdvancedDropDownFilters.AdvancedDropDownRoute.UserDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.AdvancedDropDownFilters.AdvancedDropDownRoute.MenuDataList, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "name", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.AuthenticationRoute.Auth, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "username", Type = "string",  BinderType = "" },
                        new ParameterInfo() {Name = "password", Type = "string",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.IconRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.IconRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Icon.IconPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.IconRoute.AddDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.LoginRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.LoginRoute.Error, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.AddDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.RowContextMenu, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.SaveCreate, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.EditDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.SaveEdit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Menu.MenuEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.Forbidden, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.MenuRoute.Normal, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.PermissionRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.PermissionRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Permission.PermissionPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.AddDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.RowContextMenu, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RolePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.EditDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.SaveEdit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RoleEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.Forbidden, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.Normal, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.RoleRoute.SaveCreate, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.Role.RoleCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.Index, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.GridStateChange, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.RowContextMenu, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.AddDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.SaveCreate, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserCreatePostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.SaveEdit, new RouteInfo
                {
                    HttpMethod = "POST",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "model", Type = "Core.Model.Administration.User.UserEditPostModel",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.EditDialog, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.Delete, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.Recover, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.Forbidden, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
            {Core.Mvc.Areas.Administration.Routes.UserRoute.Normal, new RouteInfo
                {
                    HttpMethod = "GET",
                    Parameters = new List<ParameterInfo>
                    {
                        new ParameterInfo() {Name = "id", Type = "int",  BinderType = "" },
                    }
                }
            },
        };
    }

    /// <summary>
    /// <see cref="Controllers.RedirectController"/>
    /// </summary>
    public class RedirectRoute
    {
        /// <summary>
        /// <see cref="Controllers.RedirectController.Index"/>
        /// </summary>
        public const string Index = "/Redirect/Index";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Index2"/>
        /// </summary>
        public const string Index2 = "/Redirect/Index2";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Login"/>
        /// </summary>
        public const string Login = "/Redirect/Login";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Widgets"/>
        /// </summary>
        public const string Widgets = "/Redirect/Widgets";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Buttons"/>
        /// </summary>
        public const string Buttons = "/Redirect/Buttons";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Calendar"/>
        /// </summary>
        public const string Calendar = "/Redirect/Calendar";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Charts"/>
        /// </summary>
        public const string Charts = "/Redirect/Charts";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Chat"/>
        /// </summary>
        public const string Chat = "/Redirect/Chat";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Error"/>
        /// </summary>
        public const string Error = "/Redirect/Error";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Gallery"/>
        /// </summary>
        public const string Gallery = "/Redirect/Gallery";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Interface"/>
        /// </summary>
        public const string Interface = "/Redirect/Interface";

        /// <summary>
        /// <see cref="Controllers.RedirectController.Invoice"/>
        /// </summary>
        public const string Invoice = "/Redirect/Invoice";
    }
}

namespace Core.Mvc.Areas.Log.Routes
{
    /// <summary>
    /// <see cref="Controllers.LogController"/>
    /// </summary>
    public class LogRoute
    {
        /// <summary>
        /// <see cref="Controllers.LogController.Index"/>
        /// </summary>
        public const string Index = "/Log/Log/Index";

        /// <summary>
        /// <see cref="Controllers.LogController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Log/Log/GridStateChange";

        /// <summary>
        /// <see cref="Controllers.LogController.Clear"/>
        /// </summary>
        public const string Clear = "/Log/Log/Clear";
    }
}

namespace Core.Mvc.Areas.AdvancedDropDownFilters
{
    /// <summary>
    /// <see cref="AdvancedDropDownController"/>
    /// </summary>
    public class AdvancedDropDownRoute
    {
        /// <summary>
        /// <see cref="AdvancedDropDownController.RoleDataList"/>
        /// </summary>
        public const string RoleDataList = "/AdvancedDropDownFilters/AdvancedDropDown/RoleDataList";

        /// <summary>
        /// <see cref="AdvancedDropDownController.UserDataList"/>
        /// </summary>
        public const string UserDataList = "/AdvancedDropDownFilters/AdvancedDropDown/UserDataList";

        /// <summary>
        /// <see cref="AdvancedDropDownController.MenuDataList"/>
        /// </summary>
        public const string MenuDataList = "/AdvancedDropDownFilters/AdvancedDropDown/MenuDataList";
    }
}

namespace Core.Mvc.Areas.Administration.Routes
{
    /// <summary>
    /// <see cref="Controllers.AuthenticationController"/>
    /// </summary>
    public class AuthenticationRoute
    {
        /// <summary>
        /// <see cref="Controllers.AuthenticationController.Auth"/>
        /// </summary>
        public const string Auth = "/Administration/Authentication/Auth";
    }

    /// <summary>
    /// <see cref="Controllers.IconController"/>
    /// </summary>
    public class IconRoute
    {
        /// <summary>
        /// <see cref="Controllers.IconController.Index"/>
        /// </summary>
        public const string Index = "/Administration/Icon/Index";

        /// <summary>
        /// <see cref="Controllers.IconController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Administration/Icon/GridStateChange";

        /// <summary>
        /// <see cref="Controllers.IconController.AddDialog"/>
        /// </summary>
        public const string AddDialog = "/Administration/Icon/AddDialog";
    }

    /// <summary>
    /// <see cref="Controllers.LoginController"/>
    /// </summary>
    public class LoginRoute
    {
        /// <summary>
        /// <see cref="Controllers.LoginController.Index"/>
        /// </summary>
        public const string Index = "/Administration/Login/Index";

        /// <summary>
        /// <see cref="Controllers.LoginController.Error"/>
        /// </summary>
        public const string Error = "/Administration/Login/Error";
    }

    /// <summary>
    /// <see cref="Controllers.MenuController"/>
    /// </summary>
    public class MenuRoute
    {
        /// <summary>
        /// <see cref="Controllers.MenuController.Index"/>
        /// </summary>
        public const string Index = "/Administration/Menu/Index";

        /// <summary>
        /// <see cref="Controllers.MenuController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Administration/Menu/GridStateChange";

        /// <summary>
        /// <see cref="Controllers.MenuController.AddDialog"/>
        /// </summary>
        public const string AddDialog = "/Administration/Menu/AddDialog";

        /// <summary>
        /// <see cref="Controllers.MenuController.RowContextMenu"/>
        /// </summary>
        public const string RowContextMenu = "/Administration/Menu/RowContextMenu";

        /// <summary>
        /// <see cref="Controllers.MenuController.SaveCreate"/>
        /// </summary>
        public const string SaveCreate = "/Administration/Menu/SaveCreate";

        /// <summary>
        /// <see cref="Controllers.MenuController.EditDialog"/>
        /// </summary>
        public const string EditDialog = "/Administration/Menu/EditDialog";

        /// <summary>
        /// <see cref="Controllers.MenuController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/Administration/Menu/SaveEdit";

        /// <summary>
        /// <see cref="Controllers.MenuController.Delete"/>
        /// </summary>
        public const string Delete = "/Administration/Menu/Delete";

        /// <summary>
        /// <see cref="Controllers.MenuController.Recover"/>
        /// </summary>
        public const string Recover = "/Administration/Menu/Recover";

        /// <summary>
        /// <see cref="Controllers.MenuController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/Administration/Menu/Forbidden";

        /// <summary>
        /// <see cref="Controllers.MenuController.Normal"/>
        /// </summary>
        public const string Normal = "/Administration/Menu/Normal";
    }

    /// <summary>
    /// <see cref="Controllers.PermissionController"/>
    /// </summary>
    public class PermissionRoute
    {
        /// <summary>
        /// <see cref="Controllers.PermissionController.Index"/>
        /// </summary>
        public const string Index = "/Administration/Permission/Index";

        /// <summary>
        /// <see cref="Controllers.PermissionController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Administration/Permission/GridStateChange";
    }

    /// <summary>
    /// <see cref="Controllers.RoleController"/>
    /// </summary>
    public class RoleRoute
    {
        /// <summary>
        /// <see cref="Controllers.RoleController.Index"/>
        /// </summary>
        public const string Index = "/Administration/Role/Index";

        /// <summary>
        /// <see cref="Controllers.RoleController.AddDialog"/>
        /// </summary>
        public const string AddDialog = "/Administration/Role/AddDialog";

        /// <summary>
        /// <see cref="Controllers.RoleController.RowContextMenu"/>
        /// </summary>
        public const string RowContextMenu = "/Administration/Role/RowContextMenu";

        /// <summary>
        /// <see cref="Controllers.RoleController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Administration/Role/GridStateChange";

        /// <summary>
        /// <see cref="Controllers.RoleController.Delete"/>
        /// </summary>
        public const string Delete = "/Administration/Role/Delete";

        /// <summary>
        /// <see cref="Controllers.RoleController.EditDialog"/>
        /// </summary>
        public const string EditDialog = "/Administration/Role/EditDialog";

        /// <summary>
        /// <see cref="Controllers.RoleController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/Administration/Role/SaveEdit";

        /// <summary>
        /// <see cref="Controllers.RoleController.Recover"/>
        /// </summary>
        public const string Recover = "/Administration/Role/Recover";

        /// <summary>
        /// <see cref="Controllers.RoleController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/Administration/Role/Forbidden";

        /// <summary>
        /// <see cref="Controllers.RoleController.Normal"/>
        /// </summary>
        public const string Normal = "/Administration/Role/Normal";

        /// <summary>
        /// <see cref="Controllers.RoleController.SaveCreate"/>
        /// </summary>
        public const string SaveCreate = "/Administration/Role/SaveCreate";
    }

    /// <summary>
    /// <see cref="Controllers.UserController"/>
    /// </summary>
    public class UserRoute
    {
        /// <summary>
        /// <see cref="Controllers.UserController.Index"/>
        /// </summary>
        public const string Index = "/Administration/User/Index";

        /// <summary>
        /// <see cref="Controllers.UserController.GridStateChange"/>
        /// </summary>
        public const string GridStateChange = "/Administration/User/GridStateChange";

        /// <summary>
        /// <see cref="Controllers.UserController.RowContextMenu"/>
        /// </summary>
        public const string RowContextMenu = "/Administration/User/RowContextMenu";

        /// <summary>
        /// <see cref="Controllers.UserController.AddDialog"/>
        /// </summary>
        public const string AddDialog = "/Administration/User/AddDialog";

        /// <summary>
        /// <see cref="Controllers.UserController.SaveCreate"/>
        /// </summary>
        public const string SaveCreate = "/Administration/User/SaveCreate";

        /// <summary>
        /// <see cref="Controllers.UserController.SaveEdit"/>
        /// </summary>
        public const string SaveEdit = "/Administration/User/SaveEdit";

        /// <summary>
        /// <see cref="Controllers.UserController.EditDialog"/>
        /// </summary>
        public const string EditDialog = "/Administration/User/EditDialog";

        /// <summary>
        /// <see cref="Controllers.UserController.Delete"/>
        /// </summary>
        public const string Delete = "/Administration/User/Delete";

        /// <summary>
        /// <see cref="Controllers.UserController.Recover"/>
        /// </summary>
        public const string Recover = "/Administration/User/Recover";

        /// <summary>
        /// <see cref="Controllers.UserController.Forbidden"/>
        /// </summary>
        public const string Forbidden = "/Administration/User/Forbidden";

        /// <summary>
        /// <see cref="Controllers.UserController.Normal"/>
        /// </summary>
        public const string Normal = "/Administration/User/Normal";
    }
}
