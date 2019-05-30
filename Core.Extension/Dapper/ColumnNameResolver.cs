using System;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using static Dapper.SimpleCRUD;

namespace Dapper
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
            }

            return columnName;
        }

        public virtual string ResolveColumnName<T>(string name)
        {
            return Encapsulate(name);
        }
    }
}
