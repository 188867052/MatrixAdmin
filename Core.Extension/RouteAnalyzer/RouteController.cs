﻿using Microsoft.AspNetCore.Mvc;

namespace Core.Extension.RouteAnalyzer
{
    public class RouteController : Controller
    {
        private readonly IRouteAnalyzer _routeAnalyzer;

        public RouteController(IRouteAnalyzer routeAnalyzer)
        {
            this._routeAnalyzer = routeAnalyzer;
        }

        [HttpGet]
        public JsonResult ShowAllRoutes()
        {
            var infos = this._routeAnalyzer.GetAllRouteInformations();
            return this.Json(infos);
        }
    }
}