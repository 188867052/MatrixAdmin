using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Dapper
{
    public static partial class SimpleCRUD
    {
        public class ColumnNameResolver : IColumnNameResolver
        {
            public virtual string ResolveColumnName(PropertyInfo propertyInfo, string name = default)
            {
                var columnName = Encapsulate(name);

                var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
                if (columnattr != null)
                {
                    columnName = Encapsulate(name);
                    if (Debugger.IsAttached)
                    {
                        Trace.WriteLine(string.Format("Column name for type overridden from {0} to {1}", propertyInfo.Name, columnName));
                    }
                }

                return columnName;
            }
        }
    }
}
