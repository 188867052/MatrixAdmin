using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCore.RouteAnalyzers.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteAnalyzer _routeAnalyzer;

        public RouteController(IRouteAnalyzer routeAnalyzer)
        {
            _routeAnalyzer = routeAnalyzer;
        }

        public IActionResult ShowAllRoutes()
        {
            var infos = _routeAnalyzer.GetAllRouteInformations();
            return Content(JsonConvert.SerializeObject(infos));
        }
    }
}
