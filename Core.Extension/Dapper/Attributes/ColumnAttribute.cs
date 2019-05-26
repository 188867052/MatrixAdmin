using System;

namespace Dapper
{
    /// <summary>
    /// Optional Column attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the table name of a poco.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAttribute"/> class.
        /// Optional Column attribute.
        /// </summary>
        /// <param name="columnName"></param>
        public ColumnAttribute(string columnName)
        {
            this.Name = columnName;
        }

        /// <summary>
        /// Name of the column.
        /// </summary>
        public string Name { get; private set; }
    }
}
