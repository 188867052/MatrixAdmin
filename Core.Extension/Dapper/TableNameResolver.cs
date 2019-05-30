﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using static Dapper.SimpleCRUD;

namespace Dapper
{
    public class TableNameResolver : ITableNameResolver
    {
        public virtual string ResolveTableName(Type type)
        {
            var tableName = Encapsulate(type.Name);

            var tableattr = type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
            if (tableattr != null)
            {
                tableName = Encapsulate(tableattr.Name);
                try
                {
                    if (!string.IsNullOrEmpty(tableattr.Schema))
                    {
                        string schemaName = Encapsulate(tableattr.Schema);
                        tableName = string.Format("{0}.{1}", schemaName, tableName);
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
