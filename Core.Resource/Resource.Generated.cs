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
		/// Gets the localized string for Code.
		/// </summary>
		public static string Code { get { return GetResourceString("Code"); } }

		/// <summary>
		/// Gets the localized string for Color.
		/// </summary>
		public static string Color { get { return GetResourceString("Color"); } }

		/// <summary>
		/// Gets the localized string for CreatedByUserName.
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// Gets the localized string for CreatedOn.
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// Gets the localized string for Custom.
		/// </summary>
		public static string Custom { get { return GetResourceString("Custom"); } }

		/// <summary>
		/// Gets the localized string for Icon.
		/// </summary>
		public static string Icon { get { return GetResourceString("Icon"); } }

		/// <summary>
		/// Gets the localized string for Size.
		/// </summary>
		public static string Size { get { return GetResourceString("Size"); } }

		/// <summary>
		/// Gets the localized string for Status.
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
		/// Gets the localized string for Alias.
		/// </summary>
		public static string Alias { get { return GetResourceString("Alias"); } }

		/// <summary>
		/// Gets the localized string for CreatedByUserName.
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// Gets the localized string for CreatedOn.
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// Gets the localized string for IsDefaultRouter.
		/// </summary>
		public static string IsDefaultRouter { get { return GetResourceString("IsDefaultRouter"); } }

		/// <summary>
		/// Gets the localized string for Name.
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// Gets the localized string for ParentName.
		/// </summary>
		public static string ParentName { get { return GetResourceString("ParentName"); } }

		/// <summary>
		/// Gets the localized string for Sort.
		/// </summary>
		public static string Sort { get { return GetResourceString("Sort"); } }

		/// <summary>
		/// Gets the localized string for Status.
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// Gets the localized string for Url.
		/// </summary>
		public static string Url { get { return GetResourceString("Url"); } }

		/// <summary>
		/// Gets the localized string for WidgetTitle.
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
		/// Gets the localized string for ActionCode.
		/// </summary>
		public static string ActionCode { get { return GetResourceString("ActionCode"); } }

		/// <summary>
		/// Gets the localized string for CreatedByUserName.
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// Gets the localized string for CreatedOn.
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// Gets the localized string for Name.
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// Gets the localized string for Status.
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// Gets the localized string for WidgetTitle.
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
		/// Gets the localized string for CreatedByUserName.
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// Gets the localized string for CreatedOn.
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// Gets the localized string for IsBuiltin.
		/// </summary>
		public static string IsBuiltin { get { return GetResourceString("IsBuiltin"); } }

		/// <summary>
		/// Gets the localized string for IsSuperAdministrator.
		/// </summary>
		public static string IsSuperAdministrator { get { return GetResourceString("IsSuperAdministrator"); } }

		/// <summary>
		/// Gets the localized string for Name.
		/// </summary>
		public static string Name { get { return GetResourceString("Name"); } }

		/// <summary>
		/// Gets the localized string for Status.
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// Gets the localized string for WidgetTitle.
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
		/// Gets the localized string for CreatedByUserName.
		/// </summary>
		public static string CreatedByUserName { get { return GetResourceString("CreatedByUserName"); } }

		/// <summary>
		/// Gets the localized string for CreatedOn.
		/// </summary>
		public static string CreatedOn { get { return GetResourceString("CreatedOn"); } }

		/// <summary>
		/// Gets the localized string for DisplayName.
		/// </summary>
		public static string DisplayName { get { return GetResourceString("DisplayName"); } }

		/// <summary>
		/// Gets the localized string for LoginName.
		/// </summary>
		public static string LoginName { get { return GetResourceString("LoginName"); } }

		/// <summary>
		/// Gets the localized string for Status.
		/// </summary>
		public static string Status { get { return GetResourceString("Status"); } }

		/// <summary>
		/// Gets the localized string for UserType.
		/// </summary>
		public static string UserType { get { return GetResourceString("UserType"); } }

		/// <summary>
		/// Gets the localized string for WidgetTitle.
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
		/// Gets the localized string for Header.
		/// </summary>
		public static string Header { get { return GetResourceString("Header"); } }
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
		/// Gets the localized string for ErrorLog.
		/// </summary>
		public static string ErrorLog { get { return GetResourceString("ErrorLog"); } }

		/// <summary>
		/// Gets the localized string for IconManage.
		/// </summary>
		public static string IconManage { get { return GetResourceString("IconManage"); } }

		/// <summary>
		/// Gets the localized string for LogManage.
		/// </summary>
		public static string LogManage { get { return GetResourceString("LogManage"); } }

		/// <summary>
		/// Gets the localized string for MenuManage.
		/// </summary>
		public static string MenuManage { get { return GetResourceString("MenuManage"); } }

		/// <summary>
		/// Gets the localized string for PermissionManage.
		/// </summary>
		public static string PermissionManage { get { return GetResourceString("PermissionManage"); } }

		/// <summary>
		/// Gets the localized string for RoleManage.
		/// </summary>
		public static string RoleManage { get { return GetResourceString("RoleManage"); } }

		/// <summary>
		/// Gets the localized string for SystemManage.
		/// </summary>
		public static string SystemManage { get { return GetResourceString("SystemManage"); } }

		/// <summary>
		/// Gets the localized string for UserManage.
		/// </summary>
		public static string UserManage { get { return GetResourceString("UserManage"); } }
	}
}
