using Core.Shared.Utilities;
using Core.Web.Identifiers;

namespace Core.Web.JavaScript
{
    public class MethodCall
    {
        public MethodCall(string method)
        {
            Check.NotEmpty(method, nameof(method));

            this.Method = method;
            this.Id = new Identifier();
        }

        public string Method { get; }

        public Identifier Id { get; }
    }
}