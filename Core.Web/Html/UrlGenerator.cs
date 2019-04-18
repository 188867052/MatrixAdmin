using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Core.Web.Html
{
	/// <summary>
	/// Generates URLs.
	/// </summary>
	public class UrlGenerator
	{
		private readonly UrlHelper _urlHelper;

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlGenerator"/> class.
		/// </summary>
		internal UrlGenerator()
		{
			this._urlHelper = new UrlHelper(new ActionContext());
		}

		/// <summary>
		/// Generates a URL to an action method.
		/// </summary>
		/// <param name="action">The action name.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="area">The area.</param>
		/// <param name="routeValues">The route values.</param>
		/// <returns>
		/// A URL as a string.
		/// </returns>
		internal string Generate(string action, string controller, string area, RouteValueDictionary routeValues)
		{
			RouteValueDictionary routeValuesCopy = new RouteValueDictionary();
			if (routeValues != null)
			{
				routeValuesCopy = new RouteValueDictionary(routeValues);
			}

			if (!string.IsNullOrWhiteSpace(area))
			{
				routeValuesCopy.Add("area", area);
			}
			return this._urlHelper.Action(action, controller, routeValuesCopy);
		}
	}
}