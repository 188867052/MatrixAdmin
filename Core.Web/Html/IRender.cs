namespace Core.Web.Html
{
    public interface IRender
    {
        string Render();
    }

    public interface ITextRender<TPostModel, TModel>
    {
        string Render(TModel model);
    }
}
