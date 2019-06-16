using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Extension.RouteAnalyzer
{
    public class RouteAnalyzer : IRouteAnalyzer
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RouteAnalyzer(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<RouteInformation> GetAllRouteInformations()
        {
            List<RouteInformation> list = new List<RouteInformation>();

            var routes = this._actionDescriptorCollectionProvider.ActionDescriptors.Items;
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
                    info.Namespace = e.RelativePath;
                }

                // Path of Route Attribute
                if (route.AttributeRouteInfo != null)
                {
                    var e = route;
                    info.Path = $"/{e.AttributeRouteInfo.Template}";
                    foreach (var item in e.Parameters)
                    {
                        info.Parameters.Add(new ParameterInfo
                        {
                            Name = item.Name,
                            Type = item.ParameterType.Name,
                            BinderType = item.BindingInfo?.BinderType?.Name
                        });
                    }
                }

                if (route is ControllerActionDescriptor)
                {
                    var e = route as ControllerActionDescriptor;
                    info.ControllerName = e.ControllerName;
                    info.ActionName = e.ActionName;
                    info.Namespace = e.ControllerTypeInfo.AsType().Namespace;
                    if (string.IsNullOrEmpty(e.AttributeRouteInfo?.Template))
                    {
                        if (!string.IsNullOrEmpty(info.Area))
                        {
                            info.Path = $"/{info.Area}";
                        }

                        info.Path += $"/{e.ControllerName}/{e.ActionName}";
                    }
                }

                // Extract HTTP Verb
                if (route.ActionConstraints != null && route.ActionConstraints.Select(t => t.GetType()).Contains(typeof(HttpMethodActionConstraint)))
                {
                    var httpMethodAction = route.ActionConstraints.FirstOrDefault(a => a.GetType() == typeof(HttpMethodActionConstraint)) as HttpMethodActionConstraint;
                    if (httpMethodAction != null)
                    {
                        info.HttpMethod = string.Join(",", httpMethodAction.HttpMethods);
                    }
                }

                // Exclude analyzer route
                if (info.Path != Router.DefaultRoute)
                {
                    list.Add(info);
                }
            }

            return list;
        }
    }
}
