using Core.Web.JavaScript;
using System.Net;

namespace Core.Mvc.ViewConfiguration.Home
{
    public class IndexViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName
        {
            get
            {
                return "Core";
            }
        }

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
            javaScriptInitialize.AddStringInstance("leftText", WebUtility.HtmlDecode(SearchGridPage.LeftText));
            javaScriptInitialize.AddStringInstance("rightText", WebUtility.HtmlDecode(SearchGridPage.RightText));
        }
    }
}
