using System;

namespace Dapper
{
    /// <summary>
    /// Optional Readonly attribute.
    /// You can use the System.ComponentModel version in its place to specify the properties that are editable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyAttribute"/> class.
        /// Optional ReadOnly attribute.
        /// </summary>
        /// <param name="isReadOnly"></param>
        public ReadOnlyAttribute(bool isReadOnly)
        {
            this.IsReadOnly = isReadOnly;
        }

        /// <summary>
        /// Does this property persist to the database?.
        /// </summary>
        public bool IsReadOnly { get; private set; }
    }
}
