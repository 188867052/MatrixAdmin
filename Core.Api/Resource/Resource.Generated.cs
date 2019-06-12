using System.Threading;
using System.Resources;

namespace Core.Api.Resource.Controllers
{
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class AuthenticationController
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
                    resourceManager = new ResourceManager("Core.Api.Resource.Controllers.AuthenticationController", typeof(AuthenticationController).Assembly);
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
		/// Gets the localized string for Locked.
		/// </summary>
		public static string Locked { get { return GetResourceString("Locked"); } }

		/// <summary>
		/// Gets the localized string for OperateSuccess.
		/// </summary>
		public static string OperateSuccess { get { return GetResourceString("OperateSuccess"); } }

		/// <summary>
		/// Gets the localized string for PasswordWrong.
		/// </summary>
		public static string PasswordWrong { get { return GetResourceString("PasswordWrong"); } }

		/// <summary>
		/// Gets the localized string for UserDisable.
		/// </summary>
		public static string UserDisable { get { return GetResourceString("UserDisable"); } }

		/// <summary>
		/// Gets the localized string for UserNotExist.
		/// </summary>
		public static string UserNotExist { get { return GetResourceString("UserNotExist"); } }
	}
	/// <summary>
	/// A static class used to access a specific set of resources.
	/// </summary>
	public static class UserController
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
                    resourceManager = new ResourceManager("Core.Api.Resource.Controllers.UserController", typeof(UserController).Assembly);
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
		/// Gets the localized string for PasswordWrong.
		/// </summary>
		public static string PasswordWrong { get { return GetResourceString("PasswordWrong"); } }

		/// <summary>
		/// Gets the localized string for PleaseInputLoginName.
		/// </summary>
		public static string PleaseInputLoginName { get { return GetResourceString("PleaseInputLoginName"); } }

		/// <summary>
		/// Gets the localized string for UserDisable.
		/// </summary>
		public static string UserDisable { get { return GetResourceString("UserDisable"); } }

		/// <summary>
		/// Gets the localized string for UserNameHasExist.
		/// </summary>
		public static string UserNameHasExist { get { return GetResourceString("UserNameHasExist"); } }

		/// <summary>
		/// Gets the localized string for UserNotExist.
		/// </summary>
		public static string UserNotExist { get { return GetResourceString("UserNotExist"); } }
	}
}
