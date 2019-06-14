using System.Collections.Generic;

namespace Core.Api.RouteAnalyzer
{
    public interface IRouteAnalyzer
    {
        IEnumerable<RouteInformation> GetAllRouteInformations();
    }
}
