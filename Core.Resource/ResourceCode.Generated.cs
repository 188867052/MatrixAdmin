using System.Threading;
using System.Resources;

namespace Core.Resource.Error
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
                    resourceManager = new ResourceManager("Core.Resource.Error.ErrorResource", typeof(ErrorResource).Assembly);
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
