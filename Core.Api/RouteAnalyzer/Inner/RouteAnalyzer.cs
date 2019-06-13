using AspNetCore.RouteAnalyzers.Controllers;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.RouteAnalyzers.Inner
{
    public class RouteAnalyzer : IRouteAnalyzer
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RouteAnalyzer(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<RouteInformation> GetAllRouteInformations()
        {
            List<RouteInformation> ret = new List<RouteInformation>();

            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (ActionDescriptor route in routes)
            {
                RouteInformation info = new RouteInformation();

                // Area
                if (route.RouteValues.ContainsKey("area"))
                {
                    info.Area = route.RouteValues["area"];
                }

                // Path and Invocation of Razor Pages
                if (route is PageActionDescriptor)
                {
                    var e = route as PageActionDescriptor;
                    info.Path = e.ViewEnginePath;
                    info.Invocation = e.RelativePath;
                }

                // Path of Route Attribute
                if (route.AttributeRouteInfo != null)
                {
                    var e = route;
                    info.Path = $"/{e.AttributeRouteInfo.Template}";
                }

                // Path and Invocation of Controller/Action
                if (route is ControllerActionDescriptor)
                {
                    var e = route as ControllerActionDescriptor;
                    if (info.Path == "")
                    {
                        info.Path = $"/{e.ControllerName}/{e.ActionName}";
                    }
                    info.Invocation = $"{e.ControllerName}Controller.{e.ActionName}";
                }

                // Extract HTTP Verb
                if (route.ActionConstraints != null && route.ActionConstraints.Select(t => t.GetType()).Contains(typeof(HttpMethodActionConstraint)))
                {
                    HttpMethodActionConstraint httpMethodAction =
                        route.ActionConstraints.FirstOrDefault(a => a.GetType() == typeof(HttpMethodActionConstraint)) as HttpMethodActionConstraint;

                    if (httpMethodAction != null)
                    {
                        info.HttpMethod = string.Join(",", httpMethodAction.HttpMethods);
                    }
                }

                // Special controller path
                if (info.Path == $"/{nameof(RouteController).Replace("Controller", string.Empty)}/{nameof(RouteController.ShowAllRoutes)}")
                {
                    info.Path = RouteAnalyzerRouteBuilderExtensions.RoutePath;
                }

                // Additional information of invocation
                info.Invocation += $" ({route.DisplayName})";

                // Generating List
                ret.Add(info);
            }

            // Result
            return ret;
        }
    }
}
