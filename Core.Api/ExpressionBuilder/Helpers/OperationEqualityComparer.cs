using System;
using System.Collections.Generic;
using Core.Api.ExpressionBuilder.Interfaces;

namespace Core.Api.ExpressionBuilder.Helpers
{
    internal class OperationEqualityComparer : IEqualityComparer<IOperation>
    {
        public bool Equals(IOperation x, IOperation y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase) == 0
                    && x.Active && y.Active;
        }

        public int GetHashCode(IOperation obj)
        {
            return obj.Name.GetHashCode() ^ obj.Active.GetHashCode();
        }
    }
}