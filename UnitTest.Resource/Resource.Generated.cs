using System.Threading;
using System.Resources;

namespace UnitTest.Resource.Areas
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class UnitTestResource
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
                    resourceManager = new ResourceManager("UnitTest.Resource.Areas.UnitTestResource", typeof(UnitTestResource).Assembly);
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
		/// Gets the localized string for TestAddBooleanFilter.
		/// </summary>
		public static string TestAddBooleanFilter { get { return GetResourceString("TestAddBooleanFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddIntegerEqualFilter.
		/// </summary>
		public static string TestAddIntegerEqualFilter { get { return GetResourceString("TestAddIntegerEqualFilter"); } }

		/// <summary>
		/// Gets the localized string for TestDataBaseConnection.
		/// </summary>
		public static string TestDataBaseConnection { get { return GetResourceString("TestDataBaseConnection"); } }

		/// <summary>
		/// Gets the localized string for TestStringContainsFilter.
		/// </summary>
		public static string TestStringContainsFilter { get { return GetResourceString("TestStringContainsFilter"); } }
	}
}
