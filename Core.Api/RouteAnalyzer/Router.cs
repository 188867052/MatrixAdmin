using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Core.Api.RouteAnalyzer
{
    public class Router : IRouter
    {
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
            if (context.HttpContext.Request.Path == _routePath)
            {
                var routeData = new RouteData(context.RouteData);
                routeData.Routers.Add(_defaultRouter);
                routeData.Values["controller"] = nameof(RouteController).Replace("Controller", string.Empty);
                routeData.Values["action"] = nameof(RouteController.ShowAllRoutes);
                context.RouteData = routeData;
                await _defaultRouter.RouteAsync(context);
            }
        }
    }
}
