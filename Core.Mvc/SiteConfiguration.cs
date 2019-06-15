using System.Linq;
using Core.Entity;

namespace Core.Api
{
    public static class SiteConfiguration
    {
        private static CoreContext _context;
        private static string _topHeader;
        private static string _searchGridPage;

        public static CoreContext DbContext => _context ?? (_context = new CoreContext());

        public static string TopHeader { get; } = _topHeader ?? (_topHeader = DbContext.Configuration.FirstOrDefault(o => o.Key == "TopHeader")?.Value);

        public static string SearchGridPage { get; } = _searchGridPage ?? (_searchGridPage = DbContext.Configuration.FirstOrDefault(o => o.Key == "SearchGridPage")?.Value);

        public static string Host { get; } = DbContext.Configuration.FirstOrDefault(o => o.Key == "Api.Url.IIS")?.Value;
    }
}
