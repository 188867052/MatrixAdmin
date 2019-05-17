using System.Collections.Generic;

namespace Core.Extension.ExpressionBuilder.Interfaces
{
    /// <summary>
    /// Defines a filter from which a expression will be built.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// List of statements groups that compose this filter.
        /// </summary>
        IEnumerable<IEnumerable<IFilterInfo>> Statements { get; }

        IEnumerable<IFilterInfo> FilterInfos { get; }

        IFilterStatementConnection By(IFilterInfo s2);
    }
}