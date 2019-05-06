using System;
using System.Globalization;

namespace Core.Web.Identifiers
{
    /// <summary>
    /// Generates a unique value string.
    /// </summary>
    public class UniqueValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueValue"/> class.
        /// </summary>
        internal UniqueValue()
        {
            this.Value = string.Concat("z", Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets the unique value.
        /// </summary>
        internal string Value { get; }
    }
}