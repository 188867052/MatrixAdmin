using System.Reflection;

namespace Core.Extension.Dapper
{
    public interface IColumnNameResolver
    {
        string ResolveColumnName(PropertyInfo propertyInfo, string name = default);

        string ResolveColumnName<T>(string name);
    }
}
