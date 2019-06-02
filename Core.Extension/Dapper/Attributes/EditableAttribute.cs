using System;

namespace Core.Extension.Dapper.Attributes
{
    /// <summary>
    /// Optional Editable attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the properties that are editable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableAttribute"/> class.
        /// Optional Editable attribute.
        /// </summary>
        /// <param name="isEditable">is editable.</param>
        public EditableAttribute(bool isEditable)
        {
            this.AllowEdit = isEditable;
        }

        /// <summary>
        /// Does this property persist to the database?.
        /// </summary>
        public bool AllowEdit { get; }
    }
}
