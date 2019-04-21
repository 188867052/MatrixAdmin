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
