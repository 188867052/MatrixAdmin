using AspNetCore.RouteAnalyzers.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace AspNetCore.RouteAnalyzers.Inner
{
    class Router : IRouter
    {
        IRouter m_defaultRouter;
        string m_routePath;

        public Router(IRouter defaultRouter, string routePath)
        {
            m_defaultRouter = defaultRouter;
            m_routePath = routePath;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            if (context.HttpContext.Request.Path == m_routePath)
            {
                var routeData = new RouteData(context.RouteData);
                routeData.Routers.Add(m_defaultRouter);
                routeData.Values["controller"] = nameof(RouteController).Replace("Controller", string.Empty);
                routeData.Values["action"] = nameof(RouteController.ShowAllRoutes);
                context.RouteData = routeData;
                await m_defaultRouter.RouteAsync(context);
            }
        }
    }
}
