using Core.Mvc.Areas.Administration.Routes;
using Core.Mvc.Areas.Log.Routes;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Mvc.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.Sidebar;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.IndexBaseResource;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Home
{
    public class SidebarNavigation
    {
        private static string _sidebarMenuValue;

        /// <summary>
        /// Generate sidebar menu.
        /// </summary>
        /// <returns>string.</returns>
        public static string SidebarMenu()
        {
            if (!string.IsNullOrEmpty(_sidebarMenuValue))
            {
                return _sidebarMenuValue;
            }

            SubMenu addons = new SubMenu("icon icon-file", default, SidebarNavigationResource.AddonsTitle, 5);
            addons.AddLinkButton(new LinkedAnchor(RedirectRoute.Index2, SidebarNavigationResource.Index2));
            addons.AddLinkButton(new LinkedAnchor(RedirectRoute.Gallery, SidebarNavigationResource.Gallery));
            addons.AddLinkButton(new LinkedAnchor(RedirectRoute.Calendar, SidebarNavigationResource.Calendar));
            addons.AddLinkButton(new LinkedAnchor(RedirectRoute.Invoice, SidebarNavigationResource.Invoice));
            addons.AddLinkButton(new LinkedAnchor(RedirectRoute.Chat, SidebarNavigationResource.Chat));

            SubMenu error = new SubMenu("icon icon-info-sign", default, SidebarNavigationResource.ErrorTitle, 4);
            error.AddLinkButton(new LinkedAnchor(RedirectRoute.Error + "?number=403", SidebarNavigationResource.Error403));
            error.AddLinkButton(new LinkedAnchor(RedirectRoute.Error + "?number=404", SidebarNavigationResource.Error404));
            error.AddLinkButton(new LinkedAnchor(RedirectRoute.Error + "?number=405", SidebarNavigationResource.Error405));
            error.AddLinkButton(new LinkedAnchor(RedirectRoute.Error + "?number=500", SidebarNavigationResource.Error500));

            SubMenu manage = new SubMenu("icon icon-user", default, Resources.SystemManage, 8);
            manage.AddLinkButton(new LinkedAnchor(UserRoute.Index, Resources.UserManage));
            manage.AddLinkButton(new LinkedAnchor(RoleRoute.Index, Resources.RoleManage));
            manage.AddLinkButton(new LinkedAnchor(PermissionRoute.Index, Resources.PermissionManage));
            manage.AddLinkButton(new LinkedAnchor(MenuRoute.Index, Resources.MenuManage));
            manage.AddLinkButton(new LinkedAnchor(IconRoute.Index, Resources.IconManage));

            SubMenu log = new SubMenu("icon icon-edit", default, Resources.LogManage, 2);
            log.AddLinkButton(new LinkedAnchor(LogRoute.Index, Resources.ErrorLog));

            Sidebar sidebar = new Sidebar();
            sidebar.AddSubMenu(new SubMenu("icon icon-home", RedirectRoute.Index, SidebarNavigationResource.DashboardTitle, 0, true));
            sidebar.AddSubMenu(manage);
            sidebar.AddSubMenu(log);
            sidebar.AddSubMenu(error);
            sidebar.AddSubMenu(new SubMenu("icon icon-signal", RedirectRoute.Charts, SidebarNavigationResource.Charts));
            sidebar.AddSubMenu(new SubMenu("icon icon-inbox", RedirectRoute.Widgets, SidebarNavigationResource.Widgets));
            sidebar.AddSubMenu(new SubMenu("icon icon-tint", RedirectRoute.Buttons, SidebarNavigationResource.Buttons));
            sidebar.AddSubMenu(new SubMenu("icon icon-pencil", RedirectRoute.Interface, SidebarNavigationResource.Interface));
            sidebar.AddSubMenu(addons);

            sidebar.AddSubContent(new SidebarContent("Monthly Bandwidth Transfer", 0.77, "21419.94 / 14000 MB", "progress progress-mini progress-danger active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.87, "604.44 / 4000 MB", "progress progress-mini active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.27, "614.44 / 4000 MB", "progress progress-mini active progress-striped"));

            _sidebarMenuValue = sidebar.Render();
            return _sidebarMenuValue = sidebar.Render();
        }
    }
}
