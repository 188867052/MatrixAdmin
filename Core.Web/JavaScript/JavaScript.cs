using Core.Extension;
using Core.Web.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace Core.Web.JavaScript
{
    /// <summary>
    /// JavaScript used to initialize a view instance.
    /// </summary>
    public class JavaScriptInitialize
    {
        private readonly Dictionary<string, string> _fields;
        private readonly string _globalVariableName;
        private readonly string _className;

        public JavaScriptInitialize(string globalVariableName, string className)
        {
            this._globalVariableName = globalVariableName;
            this._className = className;
            this._fields = new Dictionary<string, string>();
        }

        public void AddStringInstance(string key, string value)
        {
            this._fields.Add(key, value);
        }

        public void AddUrlInstance(string key, Url url)
        {
            AddStringInstance(key, url.Render());
        }

        public void AddFrameWorkInstance(string key, Identifier identifier)
        {
            AddStringInstance(key, identifier.Value);
        }

        public string Render()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in this._fields)
            {
                stringBuilder.Append($"{this._globalVariableName}._{keyValuePair.Key}=\"{keyValuePair.Value}\";");
            }
            string initializeCall = string.Concat(this._globalVariableName, ".initialize");
            string initializeScript = $"if({initializeCall} instanceof Function){{{initializeCall}();}}";

            return $"window.{this._globalVariableName} = new {this._className}();{stringBuilder}{initializeScript}";
        }
    }
}