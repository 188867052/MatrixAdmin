using AspNetCore.RouteAnalyzers.Inner;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.RouteAnalyzers
{
    public static class RouteAnalyzerServiceCollectionExtensions
    {
        public static IServiceCollection AddRouteAnalyzer(this IServiceCollection services)
        {
            services.AddSingleton<IRouteAnalyzer, RouteAnalyzer>();
            return services;
        }
    }

    public static class RouteAnalyzerRouteBuilderExtensions
    {
        public static string RoutePath { get; private set; } = "";

        public static IRouteBuilder MapRouteAnalyzer(this IRouteBuilder routes, string routePath)
        {
            RoutePath = routePath;
            routes.Routes.Add(new Router(routes.DefaultHandler, routePath));
            return routes;
        }
    }
}
