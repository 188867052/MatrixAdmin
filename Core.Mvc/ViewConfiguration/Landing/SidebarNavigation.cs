using Core.Mvc.Controllers;
using Core.Resource.ViewConfiguration;
using Core.Resource.ViewConfiguration.Home;
using Core.Web.Sidebar;

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
            SubMenu forms = new SubMenu("icon icon-th-list", default, SidebarNavigationResource.FormsTitle, 3);
            forms.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.FormCommon)), SidebarNavigationResource.FormCommon));
            forms.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.FormValidation)), SidebarNavigationResource.FormValidation));
            forms.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.FormWizard)), SidebarNavigationResource.FormWizard));


            SubMenu addons = new SubMenu("icon icon-file", default, SidebarNavigationResource.ErrorTitle, 5);
            addons.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Index2)), SidebarNavigationResource.Index2));
            addons.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Gallery)), SidebarNavigationResource.Gallery));
            addons.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Calendar)), SidebarNavigationResource.Calendar));
            addons.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Invoice)), SidebarNavigationResource.Invoice));
            addons.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Chat)), SidebarNavigationResource.Chat));

            SubMenu error = new SubMenu("icon icon-info-sign", default, SidebarNavigationResource.AddonsTitle, 4);
            error.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Error403)), SidebarNavigationResource.Error403));
            error.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Error404)), SidebarNavigationResource.Error404));
            error.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Error405)), SidebarNavigationResource.Error405));
            error.AddLinkButton(new LinkedAnchor(new Url(typeof(RedirectController), nameof(RedirectController.Error500)), SidebarNavigationResource.Error500));

            SubMenu manage = new SubMenu("icon icon-user", default, IndexBaseResource.SystemManage, 8);
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.UserManage)), IndexBaseResource.UserManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.RoleManage)), IndexBaseResource.RoleManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.PermissionManage)), IndexBaseResource.PermissionManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.MenuManage)), IndexBaseResource.MenuManage));
            manage.AddLinkButton(new LinkedAnchor(new Url(typeof(UserController), nameof(UserController.IconManage)), IndexBaseResource.IconManage));

            SubMenu log = new SubMenu("icon icon-edit", default, IndexBaseResource.LogManage, 2);
            log.AddLinkButton(new LinkedAnchor(new Url(typeof(LogController), nameof(LogController.Error)), IndexBaseResource.ErrorLog));

            Sidebar sidebar = new Sidebar();
            sidebar.AddSubMenu(new SubMenu("icon icon-home", new Url(typeof(RedirectController), nameof(RedirectController.Index)), SidebarNavigationResource.DashboardTitle, 0, true));
            sidebar.AddSubMenu(manage);
            sidebar.AddSubMenu(log);
            sidebar.AddSubMenu(new SubMenu("icon icon-signal", new Url(typeof(RedirectController), nameof(RedirectController.Charts)), SidebarNavigationResource.Charts));
            sidebar.AddSubMenu(new SubMenu("icon icon-inbox", new Url(typeof(RedirectController), nameof(RedirectController.Widgets)), SidebarNavigationResource.Widgets));
            sidebar.AddSubMenu(new SubMenu("icon icon-th", new Url(typeof(RedirectController), nameof(RedirectController.Tables)), SidebarNavigationResource.Tables));
            sidebar.AddSubMenu(new SubMenu("icon icon-fullscreen", new Url(typeof(RedirectController), nameof(RedirectController.Grid)), SidebarNavigationResource.Grid));
            sidebar.AddSubMenu(forms);
            sidebar.AddSubMenu(new SubMenu("icon icon-tint", new Url(typeof(RedirectController), nameof(RedirectController.Buttons)), SidebarNavigationResource.Buttons));
            sidebar.AddSubMenu(new SubMenu("icon icon-pencil", new Url(typeof(RedirectController), nameof(RedirectController.Interface)), SidebarNavigationResource.Interface));
            sidebar.AddSubMenu(addons);
            sidebar.AddSubMenu(error);

            sidebar.AddSubContent(new SidebarContent("Monthly Bandwidth Transfer", 0.77, "21419.94 / 14000 MB", "progress progress-mini progress-danger active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.87, "604.44 / 4000 MB", "progress progress-mini active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.27, "614.44 / 4000 MB", "progress progress-mini active progress-striped"));

            return sidebar.Render();
        }
    }



}
