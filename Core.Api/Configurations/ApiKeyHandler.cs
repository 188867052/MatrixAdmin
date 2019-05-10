using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Api.Configurations
{
    public class ApiKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // do custom stuff here
            return base.SendAsync(request, cancellationToken);
        }
    }
}
