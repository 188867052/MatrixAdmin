using Microsoft.AspNetCore.Mvc;

namespace Core.Api.RouteAnalyzer
{
    public class RouteController : Controller
    {
        private readonly IRouteAnalyzer _routeAnalyzer;

        public RouteController(IRouteAnalyzer routeAnalyzer)
        {
            _routeAnalyzer = routeAnalyzer;
        }

        [HttpGet]
        public JsonResult ShowAllRoutes()
        {
            var infos = _routeAnalyzer.GetAllRouteInformations();
            return Json(infos);
        }
    }
}
