using System.Net;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Core.Web.JavaScript;

namespace Core.Mvc.ViewConfiguration.Home
{
    public class IndexViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName
        {
            get
            {
                return "Index";
            }
        }

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
            javaScriptInitialize.AddStringInstance("leftText", WebUtility.HtmlDecode(SearchGridPage.LeftText));
            javaScriptInitialize.AddStringInstance("rightText", WebUtility.HtmlDecode(SearchGridPage.RightText));
        }
    }
}
