using System;
using System.Linq;
using Core.Extension.Dapper.Attributes;
using Microsoft.CSharp.RuntimeBinder;

namespace Core.Extension.Dapper
{
    public class TableNameResolver : ITableNameResolver
    {
        public virtual string ResolveTableName(Type type)
        {
            var tableName = DapperExtension.Encapsulate(type.Name);

            var tableattr = type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
            if (tableattr != null)
            {
                tableName = DapperExtension.Encapsulate(tableattr.Name);
                try
                {
                    if (!string.IsNullOrEmpty(tableattr.Schema))
                    {
                        string schemaName = DapperExtension.Encapsulate(tableattr.Schema);
                        tableName = $"{schemaName}.{tableName}";
                    }
                }
                catch (RuntimeBinderException)
                {
                    // Schema doesn't exist on this attribute.
                }
            }

            return tableName;
        }
    }
}
