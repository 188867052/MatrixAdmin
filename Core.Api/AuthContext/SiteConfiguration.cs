using System.Linq;
using Core.Entity;

namespace Core.Api.AuthContext
{
    public static class SiteConfiguration
    {
        private static CoreApiContext _context;
        private static string _topHeader;

        public static CoreApiContext Context => _context ?? (_context = new CoreApiContext());

        public static string TopHeader
        {
            get
            {
                return _topHeader ?? (_topHeader = Context.Configuration.FirstOrDefault(o => o.Key == "TopHeader")?.Value);
            }
        }
    }
}
