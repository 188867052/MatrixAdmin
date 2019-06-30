using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.Html
{
    public interface ITextRender<TPostModel, TModel>
    {
        TagHelperOutput Render(TModel model);
    }
}