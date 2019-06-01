namespace EntityFrameworkCore.Generator.Core.Providers
{
    public interface IProviderTypeMapping
    {
        IPropertyMapping ParseType(string storeTypeName);
    }
}