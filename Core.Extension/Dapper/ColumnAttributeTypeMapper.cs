using System;
using System.Linq;
using System.Reflection;
using Dapper;

namespace Core.Extension.Dapper
{
    /// <summary>
    /// Uses the Name value of the ColumnAttribute specified, otherwise maps as usual.
    /// </summary>
    /// <typeparam name="T">The type of the object that this mapper applies to.</typeparam>
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
            {
                new CustomPropertyTypeMap(typeof(T),  PropertySelector),
                new DefaultTypeMap(typeof(T))
            })
        {
        }

        private static PropertyInfo PropertySelector(Type type, string columnName)
        {
            return type.GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttributes(false)
                    .OfType<ColumnMappingAttribute>()
                    .Any(attr => attr.Name == columnName));
        }
    }
}