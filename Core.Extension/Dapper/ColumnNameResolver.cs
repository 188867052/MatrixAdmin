using System.Linq;
using System.Reflection;
using Core.Extension.Dapper.Attributes;

namespace Core.Extension.Dapper
{
    public class ColumnNameResolver : IColumnNameResolver
    {
        public virtual string ResolveColumnName(PropertyInfo propertyInfo, string name = default)
        {
            var columnName = SimpleCRUD.Encapsulate(name);

            var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
            if (columnattr != null)
            {
                columnName = SimpleCRUD.Encapsulate(name);
            }

            return columnName;
        }

        public virtual string ResolveColumnName<T>(string name)
        {
            return SimpleCRUD.Encapsulate(name);
        }
    }
}
