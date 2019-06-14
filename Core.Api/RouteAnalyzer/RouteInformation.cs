using System.Collections.Generic;

namespace Core.Api.RouteAnalyzer
{
    public class RouteInformation
    {
        public RouteInformation()
        {
            Parameters = new List<ParameterInfo>();
        }
        public string HttpMethod { get; set; }
        public string Area { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public IList<ParameterInfo> Parameters { get; set; }
        public string Path { get; set; }
        public string Namespace { get; set; }
    }
}
