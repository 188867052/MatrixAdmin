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
		/// Gets the localized string for TestAddDateTimeGreaterThanOrEqualFilters.
		/// </summary>
		public static string TestAddDateTimeGreaterThanOrEqualFilters { get { return GetResourceString("TestAddDateTimeGreaterThanOrEqualFilters"); } }

		/// <summary>
		/// Gets the localized string for TestAddDateTimeLessThanOrEqualFilter.
		/// </summary>
		public static string TestAddDateTimeLessThanOrEqualFilter { get { return GetResourceString("TestAddDateTimeLessThanOrEqualFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddIntegerEqualFilter.
		/// </summary>
		public static string TestAddIntegerEqualFilter { get { return GetResourceString("TestAddIntegerEqualFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddIntegerInArrayFilter.
		/// </summary>
		public static string TestAddIntegerInArrayFilter { get { return GetResourceString("TestAddIntegerInArrayFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringEndsWithFilter.
		/// </summary>
		public static string TestAddStringEndsWithFilter { get { return GetResourceString("TestAddStringEndsWithFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringEqualFilter.
		/// </summary>
		public static string TestAddStringEqualFilter { get { return GetResourceString("TestAddStringEqualFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringInArrayFilter.
		/// </summary>
		public static string TestAddStringInArrayFilter { get { return GetResourceString("TestAddStringInArrayFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringIsEmptyFilter.
		/// </summary>
		public static string TestAddStringIsEmptyFilter { get { return GetResourceString("TestAddStringIsEmptyFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringIsNullFilter.
		/// </summary>
		public static string TestAddStringIsNullFilter { get { return GetResourceString("TestAddStringIsNullFilter"); } }

		/// <summary>
		/// Gets the localized string for TestAddStringStartsWithFilter.
		/// </summary>
		public static string TestAddStringStartsWithFilter { get { return GetResourceString("TestAddStringStartsWithFilter"); } }

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
