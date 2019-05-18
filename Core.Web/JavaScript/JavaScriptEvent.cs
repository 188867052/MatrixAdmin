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

        public JavaScriptEvent(string func, string @class, JavaScriptEventEnum eventType = default) : this(eventType)
        {
            this._func = func;
            this._selector = $".{@class}";
        }

        public JavaScriptEvent(string func, Identifier id, JavaScriptEventEnum eventType = default) : this(eventType)
        {
            this._func = func;
            this._selector = $"#{id.Value}";
            this.Id = id;
        }

        public JavaScriptEvent(string func, JavaScriptEventEnum eventType = default) : this(eventType)
        {
            this._func = func;
            this.Id = new Identifier();
            this._selector = $"#{this.Id.Value}";
        }

        private JavaScriptEvent(JavaScriptEventEnum eventType)
        {
            this._eventType = eventType;
        }

        public Identifier Id { get; set; }

        public string Render()
        {
            string js = $"$(\"{this._selector}\").on('{JavaScriptEnumMappings.ToString(this._eventType)}',function(){{{this._func}();}});";
            return $"<script>{js}</script>";
        }
    }
}