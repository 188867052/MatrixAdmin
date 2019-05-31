using System;

namespace Core.Extension.Dapper
{
    public interface ITableNameResolver
    {
        string ResolveTableName(Type type);
    }
}
