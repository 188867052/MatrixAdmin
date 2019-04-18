using Core.Mvc.ViewConfigurations;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc
{
	public class StandardController : Controller
	{
		protected IActionResult ViewConfiguration(IndexPage index)
		{
			index.SetRoute(Controller, Action); 
			string header = index.RenderHeader();
			string body = index.RenderBody();
			return Content(header + body, "text/html");
		}

		protected string Controller
		{
			get
			{
				return this.RouteData.Values["controller"].ToString();
			}
		}

		protected string Action
		{
			get
			{
				return this.RouteData.Values["action"].ToString();
			}
		}
	}
}