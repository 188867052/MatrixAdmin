using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RouteAnalyzers
{
    public interface IRouteAnalyzer
    {
        IEnumerable<RouteInformation> GetAllRouteInformations();
    }
}
