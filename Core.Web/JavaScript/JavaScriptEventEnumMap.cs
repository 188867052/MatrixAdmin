namespace Core.Web.JavaScript
{
    public static class JavaScriptEventEnumMap
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