using System.Collections.Generic;

namespace Core.Extension.RouteAnalyzer
{
    public interface IRouteAnalyzer
    {
        IEnumerable<RouteInfo> GetAllRouteInformations();
    }
}
