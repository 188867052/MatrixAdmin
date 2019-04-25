using System.Threading;
using System.Resources;

namespace Core.Resource.ViewConfiguration.Administration
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class IconResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Administration.IconResource", typeof(IconResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [Code].
		/// </summary>
		public static string Code { get { return GetResourceString("Code"); } }

		/// <summary>
		/// 获取本地化字符串 [Color].
		/// </summary>
		public static string Color { get { return GetResourceString("Color"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedByUserName].
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedOn].
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// 获取本地化字符串 [Custom].
		/// </summary>
		public static string Custom { get { return GetResourceString("Custom"); } }

		/// <summary>
		/// 获取本地化字符串 [Icon].
		/// </summary>
		public static string Icon { get { return GetResourceString("Icon"); } }

		/// <summary>
		/// 获取本地化字符串 [Size].
		/// </summary>
		public static string Size { get { return GetResourceString("Size"); } }

		/// <summary>
		/// 获取本地化字符串 [Status].
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class MenuIndexResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Administration.MenuIndexResource", typeof(MenuIndexResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [Alias].
		/// </summary>
		public static string Alias { get { return GetResourceString("Alias"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedByUserName].
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedOn].
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// 获取本地化字符串 [IsDefaultRouter].
		/// </summary>
		public static string IsDefaultRouter { get { return GetResourceString("IsDefaultRouter"); } }

		/// <summary>
		/// 获取本地化字符串 [Name].
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// 获取本地化字符串 [ParentName].
		/// </summary>
		public static string ParentName { get { return GetResourceString("ParentName"); } }

		/// <summary>
		/// 获取本地化字符串 [Sort].
		/// </summary>
		public static string Sort { get { return GetResourceString("Sort"); } }

		/// <summary>
		/// 获取本地化字符串 [Status].
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// 获取本地化字符串 [Url].
		/// </summary>
		public static string Url { get { return GetResourceString("Url"); } }

		/// <summary>
		/// 获取本地化字符串 [WidgetTitle].
		/// </summary>
		public static string WidgetTitle { get { return GetResourceString("WidgetTitle"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class PermissionIndexResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Administration.PermissionIndexResource", typeof(PermissionIndexResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [ActionCode].
		/// </summary>
		public static string ActionCode { get { return GetResourceString("ActionCode"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedByUserName].
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedOn].
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// 获取本地化字符串 [Name].
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// 获取本地化字符串 [Status].
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// 获取本地化字符串 [WidgetTitle].
		/// </summary>
		public static string WidgetTitle { get { return GetResourceString("WidgetTitle"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class RoleIndexResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Administration.RoleIndexResource", typeof(RoleIndexResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [CreatedByUserName].
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedOn].
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// 获取本地化字符串 [IsBuiltin].
		/// </summary>
		public static string IsBuiltin { get { return GetResourceString("IsBuiltin"); } }

		/// <summary>
		/// 获取本地化字符串 [IsSuperAdministrator].
		/// </summary>
		public static string IsSuperAdministrator { get { return GetResourceString("IsSuperAdministrator"); } }

		/// <summary>
		/// 获取本地化字符串 [Name].
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// 获取本地化字符串 [Status].
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// 获取本地化字符串 [WidgetTitle].
		/// </summary>
		public static string WidgetTitle { get { return GetResourceString("WidgetTitle"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class UserIndexResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Administration.UserIndexResource", typeof(UserIndexResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [CreatedByUserName].
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// 获取本地化字符串 [CreatedOn].
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// 获取本地化字符串 [DisplayName].
		/// </summary>
		public static string DisplayName { get { return GetResourceString("DisplayName"); } }

		/// <summary>
		/// 获取本地化字符串 [LoginName].
		/// </summary>
		public static string LoginName { get { return GetResourceString("LoginName"); } }

		/// <summary>
		/// 获取本地化字符串 [Status].
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// 获取本地化字符串 [UserType].
		/// </summary>
		public static string UserType { get { return GetResourceString("UserType"); } }

		/// <summary>
		/// 获取本地化字符串 [WidgetTitle].
		/// </summary>
		public static string WidgetTitle { get { return GetResourceString("WidgetTitle"); } }
	}
}

namespace Core.Resource.ViewConfiguration.Error
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class ErrorResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Error.ErrorResource", typeof(ErrorResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [Header].
		/// </summary>
		public static string Header { get { return GetResourceString("Header"); } }
	}
}

namespace Core.Resource.ViewConfiguration.Home
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class SidebarNavigationResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Home.SidebarNavigationResource", typeof(SidebarNavigationResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [AddonsTitle].
		/// </summary>
		public static string AddonsTitle { get { return GetResourceString("AddonsTitle"); } }

		/// <summary>
		/// 获取本地化字符串 [Buttons].
		/// </summary>
		public static string Buttons { get { return GetResourceString("Buttons"); } }

		/// <summary>
		/// 获取本地化字符串 [Calendar].
		/// </summary>
		public static string Calendar { get { return GetResourceString("Calendar"); } }

		/// <summary>
		/// 获取本地化字符串 [Charts].
		/// </summary>
		public static string Charts { get { return GetResourceString("Charts"); } }

		/// <summary>
		/// 获取本地化字符串 [Chat].
		/// </summary>
		public static string Chat { get { return GetResourceString("Chat"); } }

		/// <summary>
		/// 获取本地化字符串 [DashboardTitle].
		/// </summary>
		public static string DashboardTitle { get { return GetResourceString("DashboardTitle"); } }

		/// <summary>
		/// 获取本地化字符串 [Error403].
		/// </summary>
		public static string Error403 { get { return GetResourceString("Error403"); } }

		/// <summary>
		/// 获取本地化字符串 [Error404].
		/// </summary>
		public static string Error404 { get { return GetResourceString("Error404"); } }

		/// <summary>
		/// 获取本地化字符串 [Error405].
		/// </summary>
		public static string Error405 { get { return GetResourceString("Error405"); } }

		/// <summary>
		/// 获取本地化字符串 [Error500].
		/// </summary>
		public static string Error500 { get { return GetResourceString("Error500"); } }

		/// <summary>
		/// 获取本地化字符串 [ErrorTitle].
		/// </summary>
		public static string ErrorTitle { get { return GetResourceString("ErrorTitle"); } }

		/// <summary>
		/// 获取本地化字符串 [FormCommon].
		/// </summary>
		public static string FormCommon { get { return GetResourceString("FormCommon"); } }

		/// <summary>
		/// 获取本地化字符串 [FormsTitle].
		/// </summary>
		public static string FormsTitle { get { return GetResourceString("FormsTitle"); } }

		/// <summary>
		/// 获取本地化字符串 [FormValidation].
		/// </summary>
		public static string FormValidation { get { return GetResourceString("FormValidation"); } }

		/// <summary>
		/// 获取本地化字符串 [FormWizard].
		/// </summary>
		public static string FormWizard { get { return GetResourceString("FormWizard"); } }

		/// <summary>
		/// 获取本地化字符串 [Gallery].
		/// </summary>
		public static string Gallery { get { return GetResourceString("Gallery"); } }

		/// <summary>
		/// 获取本地化字符串 [Grid].
		/// </summary>
		public static string Grid { get { return GetResourceString("Grid"); } }

		/// <summary>
		/// 获取本地化字符串 [Index2].
		/// </summary>
		public static string Index2 { get { return GetResourceString("Index2"); } }

		/// <summary>
		/// 获取本地化字符串 [Interface].
		/// </summary>
		public static string Interface { get { return GetResourceString("Interface"); } }

		/// <summary>
		/// 获取本地化字符串 [Invoice].
		/// </summary>
		public static string Invoice { get { return GetResourceString("Invoice"); } }

		/// <summary>
		/// 获取本地化字符串 [Tables].
		/// </summary>
		public static string Tables { get { return GetResourceString("Tables"); } }

		/// <summary>
		/// 获取本地化字符串 [Widgets].
		/// </summary>
		public static string Widgets { get { return GetResourceString("Widgets"); } }
	}
}

namespace Core.Resource.ViewConfiguration.Log
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class ErrorResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Log.ErrorResource", typeof(ErrorResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [Header].
		/// </summary>
		public static string Header { get { return GetResourceString("Header"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class LogResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.Log.LogResource", typeof(LogResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [CreateTime].
		/// </summary>
		public static string CreateTime { get { return GetResourceString("CreateTime"); } }

		/// <summary>
		/// 获取本地化字符串 [ID].
		/// </summary>
		public static string ID { get { return GetResourceString("ID"); } }

		/// <summary>
		/// 获取本地化字符串 [Message].
		/// </summary>
		public static string Message { get { return GetResourceString("Message"); } }
	}
}

namespace Core.Resource.ViewConfiguration
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class IndexBaseResource
	{
        private static ResourceManager resourceManager;
        
        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
        /// </summary>
        private static ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceManager, null)) 
				{
                    resourceManager = new ResourceManager("Core.Resource.ViewConfiguration.IndexBaseResource", typeof(IndexBaseResource).Assembly);
                }
                return resourceManager;
            }
        }
        
        /// <summary>
        /// Returns the formatted resource string.
        /// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The localized resource string.</returns>
        private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
            return ResourceManager.GetString(key, culture);
        }
		
		/// <summary>
		/// 获取本地化字符串 [ErrorLog].
		/// </summary>
		public static string ErrorLog { get { return GetResourceString("ErrorLog"); } }

		/// <summary>
		/// 获取本地化字符串 [IconManage].
		/// </summary>
		public static string IconManage { get { return GetResourceString("IconManage"); } }

		/// <summary>
		/// 获取本地化字符串 [LogManage].
		/// </summary>
		public static string LogManage { get { return GetResourceString("LogManage"); } }

		/// <summary>
		/// 获取本地化字符串 [MenuManage].
		/// </summary>
		public static string MenuManage { get { return GetResourceString("MenuManage"); } }

		/// <summary>
		/// 获取本地化字符串 [PermissionManage].
		/// </summary>
		public static string PermissionManage { get { return GetResourceString("PermissionManage"); } }

		/// <summary>
		/// 获取本地化字符串 [RoleManage].
		/// </summary>
		public static string RoleManage { get { return GetResourceString("RoleManage"); } }

		/// <summary>
		/// 获取本地化字符串 [SystemManage].
		/// </summary>
		public static string SystemManage { get { return GetResourceString("SystemManage"); } }

		/// <summary>
		/// 获取本地化字符串 [UserManage].
		/// </summary>
		public static string UserManage { get { return GetResourceString("UserManage"); } }
	}
}
