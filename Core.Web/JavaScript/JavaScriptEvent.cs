using System;
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
        private readonly Identifier _id;
        private readonly string _func;
        private readonly string _class;

        public JavaScriptEvent(string func, string @class, JavaScriptEventEnum eventType = default)
        {
            this._eventType = eventType;
            this._func = func;
            this._class = @class;
        }

        public JavaScriptEvent(string func, Identifier id, JavaScriptEventEnum eventType = default)
        {
            this._eventType = eventType;
            this._func = func;
            this._id = id;
        }

        public string Render()
        {
            string js = default;
            if (this._func != null)
            {
                if (this._id != default)
                {
                    js = $"$(\"#{this._id.Value}\").on('{EnumMappings.ToString(this._eventType)}',function(){{{this._func}();}});";
                }
                else if (this._class != default)
                {
                    js = $"$(\".{this._class}\").on('{EnumMappings.ToString(this._eventType)}',function(){{{this._func}();}});";
                }
                else
                {
                    throw new ArgumentException("参数错误");
                }
            }

            return $"<script>{js}</script>";
        }
    }
}