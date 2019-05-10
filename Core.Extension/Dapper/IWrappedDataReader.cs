using System.Data;

namespace Core.Extension.Dapper
{
    /// <summary>
    /// Describes a reader that controls the lifetime of both a command and a reader,
    /// exposing the downstream command/reader as properties.
    /// </summary>
    public interface IWrappedDataReader : IDataReader
    {
        /// <summary>
        /// Gets obtain the underlying reader.
        /// </summary>
        IDataReader Reader { get; }

        /// <summary>
        /// Gets obtain the underlying command.
        /// </summary>
        IDbCommand Command { get; }
    }
}
