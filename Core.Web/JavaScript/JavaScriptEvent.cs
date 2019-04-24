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

        public JavaScriptEvent(string @delegate, Identifier id = default, JavaScriptEventEnum @event = default)
        {
            this._event = @event;
            this._delegate = @delegate;
            this._id = id;
        }


        public string Render()
        {
            string @event = default;
            if (this._id != default && this._delegate !=null)
            {
                @event = $"$(\"#{this._id.Value}\").on('{this._event.EventString()}',function(){{{this._delegate}();}});";
            }

            return @event;
        }
    }
}