using System;

namespace Dapper
{
    public interface ITableNameResolver
    {
        string ResolveTableName(Type type);
    }
}
