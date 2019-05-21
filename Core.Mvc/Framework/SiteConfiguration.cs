using System.Linq;
using Core.Entity;

namespace Core.Mvc.Framework
{
    public static class SiteConfiguration
    {
        private static CoreApiContext _context;
        private static string _topHeader;

        public static CoreApiContext DbContext => _context ?? (_context = new CoreApiContext());

        public static string TopHeader { get; } = _topHeader ?? (_topHeader = DbContext.Configuration.FirstOrDefault(o => o.Key == "TopHeader")?.Value);

        public static string Host { get; } = DbContext.Configuration.FirstOrDefault(o => o.Key == "Api.Url.IIS")?.Value;
    }
}
