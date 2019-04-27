namespace Core.Web.JavaScript
{
    public interface IViewInstanceConstruction
    {
        JavaScript InitializeViewInstance();
        string InstanceClassName { get; }
    }
}