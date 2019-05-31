using System;

namespace Core.Extension.Dapper.Attributes
{
    /// <summary>
    /// Optional Table attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the table name of a poco.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        /// <param name="tableName">tableName.</param>
        public TableAttribute(string tableName)
        {
            this.Name = tableName;
        }

        /// <summary>
        /// Name of the table.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Name of the schema.
        /// </summary>
        public string Schema { get; set; }
    }
}
