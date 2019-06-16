using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Core.Extension.RouteAnalyzer
{
    public class Router : IRouter
    {
        public const string ControllerName = "Route";
        public const string ActionName = "ShowAllRoutes";
        public const string DefaultRoute = "/" + ControllerName + "/" + ActionName;
        private readonly IRouter _defaultRouter;
        private readonly string _routePath;

        public Router(IRouter defaultRouter, string routePath)
        {
            this._defaultRouter = defaultRouter;
            this._routePath = routePath;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            if (context.HttpContext.Request.Path == this._routePath)
            {
                var routeData = new RouteData(context.RouteData);
                routeData.Routers.Add(this._defaultRouter);
                routeData.Values["controller"] = ControllerName;
                routeData.Values["action"] = ActionName;
                context.RouteData = routeData;
                await this._defaultRouter.RouteAsync(context);
            }
        }
    }
}
