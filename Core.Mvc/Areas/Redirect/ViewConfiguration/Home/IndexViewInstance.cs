using System.Net;
using Core.Extension;
using Core.Mvc.Areas.AdvancedDropDown;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Home
{
    public class IndexViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Core";

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
            javaScriptInitialize.AddStringInstance("leftText", WebUtility.HtmlDecode(SearchGridPage.LeftText));
            javaScriptInitialize.AddStringInstance("rightText", WebUtility.HtmlDecode(SearchGridPage.RightText));

            var url = new Url(nameof(AdvancedDropDown), typeof(AdvancedDropDownController), nameof(AdvancedDropDownController.GetRoleDataList));
            javaScriptInitialize.AddUrlInstance("getRoleDataList", url);
        }
    }
}
