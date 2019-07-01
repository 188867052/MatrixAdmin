using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.Html
{
    public interface IHtmlContent<TPostModel, TModel>
    {
        TagHelperOutput Render(TModel model);
    }
}