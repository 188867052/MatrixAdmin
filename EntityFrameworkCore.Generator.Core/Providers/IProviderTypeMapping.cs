namespace EntityFrameworkCore.Generator.Providers
{
    public interface IProviderTypeMapping
    {
        IPropertyMapping ParseType(string storeTypeName);
    }
}