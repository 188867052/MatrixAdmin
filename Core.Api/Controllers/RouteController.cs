using Core.Extension.RouteAnalyzer;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteAnalyzer _routeAnalyzer;

        public RouteController(IRouteAnalyzer routeAnalyzer)
        {
            this._routeAnalyzer = routeAnalyzer;
        }

        [HttpGet]
        [Route(Router.DefaultRoute)]
        public JsonResult ShowAllRoutes()
        {
            var infos = this._routeAnalyzer.GetAllRouteInformations();
            return this.Json(infos);
        }
    }
}
