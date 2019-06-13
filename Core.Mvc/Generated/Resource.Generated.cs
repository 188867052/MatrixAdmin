using System.Threading;
using System.Resources;

namespace Core.Mvc.Generated
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
                    resourceManager = new ResourceManager("Core.Mvc.Generated.IndexBaseResource", typeof(IndexBaseResource).Assembly);
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
