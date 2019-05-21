using System.Net;
using Core.Extension;
using Core.Mvc.Areas.AdvancedDropDown;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Home
{
    public class IndexViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Core";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            initialize.AddStringInstance("leftText", WebUtility.HtmlDecode(SearchGridPage<object>.LeftText));
            initialize.AddStringInstance("rightText", WebUtility.HtmlDecode(SearchGridPage<object>.RightText));

            var url = new Url(nameof(AdvancedDropDown), typeof(AdvancedDropDownController), nameof(AdvancedDropDownController.RoleDataList));
            initialize.AddUrlInstance("getRoleDataListUrl", url);
        }
    }
}
