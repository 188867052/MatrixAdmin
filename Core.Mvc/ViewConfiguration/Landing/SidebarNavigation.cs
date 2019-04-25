using Core.Mvc.Controllers;
using Core.Resource.ViewConfiguration;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.ViewConfiguration.Landing
{
    public class SidebarNavigation
    {
        /// <summary>
        /// Generate sidebar menu.
        /// </summary>
        /// <returns></returns>
        public string GenerateSidebarMenu()
        {
            SubMenu forms = new SubMenu("icon icon-th-list", "", "Forms", 3);
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formcommon", "Basic Form"));
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formvalidation", "Form with Validation"));
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formwizard", "Form with Wizard"));


            SubMenu addons = new SubMenu("icon icon-file", "", "Addons", 5);
            addons.AddLinkButton(new LinkedAnchor("/Redirect/index2", "Dashboard2"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/gallery", "Gallery"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/calendar", "Calendar"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/invoice", "Invoice"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/chat", "Chat option"));

            SubMenu error = new SubMenu("icon icon-info-sign", "", "Error", 4);
            error.AddLinkButton(new LinkedAnchor("/Redirect/error403", "Error 403"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error404", "Error 404"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error405", "Error 405"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error500", "Error 500"));

            SubMenu manage = new SubMenu("icon icon-user", "", IndexBaseResource.SystemManage, 8);
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.UserManage)), IndexBaseResource.UserManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.RoleManage)), IndexBaseResource.RoleManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.PermissionManage)), IndexBaseResource.PermissionManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.MenuManage)), IndexBaseResource.MenuManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.IconManage)), IndexBaseResource.IconManage));

            SubMenu log = new SubMenu("icon icon-edit", "", IndexBaseResource.LogManage, 2);
            log.AddLinkButton(new LinkedAnchor(new Url(typeof(LogController), nameof(LogController.Error)), IndexBaseResource.ErrorLog));

            Sidebar sidebar = new Sidebar();
            sidebar.AddSubMenu(new SubMenu("icon icon-home", "/Redirect/index", "Dashboard", 0, true));
            sidebar.AddSubMenu(manage);
            sidebar.AddSubMenu(log);
            sidebar.AddSubMenu(new SubMenu("icon icon-signal", "/Redirect/charts", "Charts &amp; Graphs"));
            sidebar.AddSubMenu(new SubMenu("icon icon-inbox", "/Redirect/widgets", "Widgets"));
            sidebar.AddSubMenu(new SubMenu("icon icon-th", "/Redirect/tables", "Tables"));
            sidebar.AddSubMenu(new SubMenu("icon icon-fullscreen", "/Redirect/grid", "Full width"));
            sidebar.AddSubMenu(forms);
            sidebar.AddSubMenu(new SubMenu("icon icon-tint", "/Redirect/buttons", "Buttons &amp; Icons"));
            sidebar.AddSubMenu(new SubMenu("icon icon-pencil", "/Redirect/interface", "Elements"));
            sidebar.AddSubMenu(addons);
            sidebar.AddSubMenu(error);

            sidebar.AddSubContent(new SidebarContent("Monthly Bandwidth Transfer", 0.77, "21419.94 / 14000 MB", "progress progress-mini progress-danger active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.87, "604.44 / 4000 MB", "progress progress-mini active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.27, "614.44 / 4000 MB", "progress progress-mini active progress-striped"));

            return sidebar.Render();
        }
    }



}
