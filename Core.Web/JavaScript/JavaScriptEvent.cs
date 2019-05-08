using Core.Web.Enums;
using Core.Web.Identifiers;

namespace Core.Web.JavaScript
{
    /// <summary>
    /// JavaScript used to initialize a view instance.
    /// </summary>
    public class JavaScriptEvent
    {
        private readonly JavaScriptEventEnum _eventType;
        private readonly string _selector;
        private readonly string _func;

        public JavaScriptEvent(string func, string @class, JavaScriptEventEnum eventType = default)
        {
            this._eventType = eventType;
            this._func = func;
            this._selector = $".{@class}";
        }

        public JavaScriptEvent(string func, Identifier id, JavaScriptEventEnum eventType = default)
        {
            this._eventType = eventType;
            this._func = func;
            this._selector = $"#{id.Value}";
        }

        public string Render()
        {
            string js = $"$(\"{this._selector}\").on('{EnumMappings.ToString(this._eventType)}',function(){{{this._func}();}});";
            return $"<script>{js}</script>";
        }
    }
}