using System;

namespace Dapper
{
    public static partial class SimpleCRUD
    {
        public interface ITableNameResolver
        {
            string ResolveTableName(Type type);
        }
    }
}
