using System.Reflection;

namespace Dapper
{
    public static partial class SimpleCRUD
    {
        public interface IColumnNameResolver
        {
            string ResolveColumnName(PropertyInfo propertyInfo, string name = default);

            string ResolveColumnName<T>(string name);
        }
    }
}
