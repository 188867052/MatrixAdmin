using System;
using System.Reflection;

namespace Core.Extension.Dapper
{
    public static partial class SqlMapper
    {
        /// <summary>
        /// Implements this interface to provide custom member mapping.
        /// </summary>
        public interface IMemberMap
        {
            /// <summary>
            /// Gets source DataReader column name.
            /// </summary>
            string ColumnName { get; }

            /// <summary>
            ///  Gets target member type.
            /// </summary>
            Type MemberType { get; }

            /// <summary>
            /// Gets target property.
            /// </summary>
            PropertyInfo Property { get; }

            /// <summary>
            /// Gets target field.
            /// </summary>
            FieldInfo Field { get; }

            /// <summary>
            /// Gets target constructor parameter.
            /// </summary>
            ParameterInfo Parameter { get; }
        }
    }
}
