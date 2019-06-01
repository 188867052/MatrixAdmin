using System.Linq;
using System.Reflection;
using Core.Extension.Dapper.Attributes;

namespace Core.Extension.Dapper
{
    public class ColumnNameResolver : IColumnNameResolver
    {
        public virtual string ResolveColumnName(PropertyInfo propertyInfo, string name = default)
        {
            var columnName = DapperExtension.Encapsulate(name);

            dynamic columnAttribute = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
            if (columnAttribute != null)
            {
                columnName = DapperExtension.Encapsulate(name);
            }

            return columnName;
        }

        public virtual string ResolveColumnName<T>(string name)
        {
            return DapperExtension.Encapsulate(name);
        }
    }
}
