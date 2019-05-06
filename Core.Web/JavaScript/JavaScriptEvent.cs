using System;
using Core.Web.Identifiers;

namespace Core.Web.JavaScript
{
    /// <summary>
    /// JavaScript used to initialize a view instance.
    /// </summary>
    public class JavaScriptEvent
    {
        private readonly JavaScriptEventEnum _event;
        private readonly Identifier _id;
        private readonly string _delegate;
        private readonly string _class;

        public JavaScriptEvent(string @delegate, string @class = default, Identifier id = default, JavaScriptEventEnum @event = default)
        {
            this._event = @event;
            this._delegate = @delegate;
            this._id = id;
            this._class = @class;
        }

        public string Render()
        {
            string @event = default;
            if (this._delegate != null)
            {
                if (this._id != default)
                {
                    @event = $"$(\"#{this._id.Value}\").on('{this._event.EventString()}',function(){{{this._delegate}();}});";
                }
                else if (this._class != default)
                {
                    @event = $"$(\".{this._class}\").on('{this._event.EventString()}',function(){{{this._delegate}();}});";
                }
                else
                {
                    throw new ArgumentException("参数错误");
                }
            }

            return $"<script>{@event}</script>";
        }
    }
}