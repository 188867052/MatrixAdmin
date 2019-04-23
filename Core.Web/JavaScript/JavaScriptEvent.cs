using System.Collections.Generic;
using System.Text;

namespace Core.Web.JavaScript
{
    /// <summary>
    /// JavaScript used to initialize a view instance.
    /// </summary>
    public class JavaScriptEvent
    {
        public JavaScriptEventEnum Event { get; set; }

        public string Delegate { get; set; }

        public JavaScriptEvent(string @delegate, JavaScriptEventEnum @event = default)
        {
            this.Event = @event;
            this.Delegate = @delegate;
        }

        public string Render()
        {
            return $"$(\".form-control\").on('{Event.EventString()}',function(){{{Delegate};}});";
        }
    }

    public static class EnumMap
    {
        public static string EventString(this JavaScriptEventEnum @event)
        {
            switch (@event)
            {
                case JavaScriptEventEnum.click:
                    return "click";
                default:
                    return @event.ToString();
            }
        }
    }
}