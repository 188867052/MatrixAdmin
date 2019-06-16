using Core.Api.Routes;
using Core.Extension.RouteAnalyzer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Collections.Generic;

namespace Core.Api.Framework
{
    public class RouteController : Controller
    {
        private readonly IRouteAnalyzer _routeAnalyzer;

        public RouteController(IRouteAnalyzer routeAnalyzer)
        {
            _routeAnalyzer = routeAnalyzer;
        }

        [HttpGet]
        [Route(Router.DefaultRoute)]
        public JsonResult ShowAllRoutes()
        {
            var infos = _routeAnalyzer.GetAllRouteInformations();
            return Json(infos);
        }

        public static dynamic GetRouteInfo(string route)
        {
            return Cache.Dictionary[route];
        }

        public static HttpMethod GetHttpMethod(string route)
        {
            string HttpMethod = GetRouteInfo(route).HttpMethod;
            if (HttpMethod == "GET")
            {
                return Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod.Get;
            }
            else
            {
                return Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod.Post;
            }
        }

        public static IList<ParameterInfo> GetParameterInfo(string route)
        {
            return GetRouteInfo(route).Parameters;
        }
    }
}
