using Core.Extension;
using Core.Web.Identifiers;
using System;
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

        public JavaScriptInitialize(string globalVariableName, string className)
        {
            this.GlobalVariableName = globalVariableName;
            this.ClassName = className;
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

        public string GlobalVariableName { get; set; }

        public string ClassName { get; set; }

        public string Render()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in this._fields)
            {
                stringBuilder.Append($"{this.GlobalVariableName}._{keyValuePair.Key}=\"{keyValuePair.Value}\";");
            }
            string initializeCall = string.Concat(this.GlobalVariableName, ".initialize");
            string initializeScript = $"if({initializeCall} instanceof Function){{{initializeCall}();}}";

            return $"window.{this.GlobalVariableName} = new {this.ClassName}();{stringBuilder}{initializeScript}";
        }

        public void AddFrameWorkInstance(string key, Identifier identifier)
        {
            AddStringInstance(key, identifier.Value);
        }
    }
}