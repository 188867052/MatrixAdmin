using Core.Web.Identifiers;

namespace Core.Web.JavaScript
{
    public class MethodCall
    {
        public MethodCall(string method)
        {
            this.Method = method;
            this.Id = new Identifier();
        }

        public string Method { get; }

        public Identifier Id { get; }
    }
}