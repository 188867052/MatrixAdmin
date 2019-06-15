using Core.Extension.RouteAnalyzer;

namespace Core.Api.Controllers
{
    public class RouteController : Extension.RouteAnalyzer.RouteController
    {
        public RouteController(IRouteAnalyzer routeAnalyzer) : base(routeAnalyzer)
        {
        }
    }
}
