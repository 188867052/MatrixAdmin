using System;
using System.Reflection;

namespace Core.Extension.Dapper
{
    /// <summary>
    /// Represents simple member map for one of target parameter or property or field to source DataReader column.
    /// </summary>
    internal sealed class SimpleMemberMap : SqlMapper.IMemberMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMemberMap"/> class.
        /// Creates instance for simple property mapping.
        /// </summary>
        /// <param name="columnName">DataReader column name.</param>
        /// <param name="property">Target property.</param>
        public SimpleMemberMap(string columnName, PropertyInfo property)
        {
            this.ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            this.Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMemberMap"/> class.
        /// Creates instance for simple field mapping.
        /// </summary>
        /// <param name="columnName">DataReader column name.</param>
        /// <param name="field">Target property.</param>
        public SimpleMemberMap(string columnName, FieldInfo field)
        {
            this.ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            this.Field = field ?? throw new ArgumentNullException(nameof(field));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMemberMap"/> class.
        /// Creates instance for simple constructor parameter mapping.
        /// </summary>
        /// <param name="columnName">DataReader column name.</param>
        /// <param name="parameter">Target constructor parameter.</param>
        public SimpleMemberMap(string columnName, ParameterInfo parameter)
        {
            this.ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            this.Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        /// <summary>
        /// DataReader column name.
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// Target member type.
        /// </summary>
        public Type MemberType => this.Field?.FieldType ?? this.Property?.PropertyType ?? this.Parameter?.ParameterType;

        /// <summary>
        /// Target property.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Target field.
        /// </summary>
        public FieldInfo Field { get; }

        /// <summary>
        /// Target constructor parameter.
        /// </summary>
        public ParameterInfo Parameter { get; }
    }
}
