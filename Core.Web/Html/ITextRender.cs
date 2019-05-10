namespace Core.Web.Html
{
    public interface ITextRender<TPostModel, TModel>
    {
        string Render(TModel model);
    }
}