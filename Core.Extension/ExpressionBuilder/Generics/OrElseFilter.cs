using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class OrElseFilter<T> : IFilter where T : class
    {
        private readonly List<IFilterInfo> _statements;

        public OrElseFilter(IFilterInfo f1, IFilterInfo f2)
        {
            this._statements = new List<IFilterInfo>();
            this._statements.Add(f1);
            var connector2 = new FilterStatementConnection(this, f1);
            connector2.Or.By(f2);
        }

        public IFilter Group => throw new NotImplementedException();

        public IEnumerable<IEnumerable<IFilterInfo>> Statements { get; }

        public IEnumerable<IFilterInfo> FilterInfos
        {
            get
            {
                return this._statements;
            }
        }

        public IFilterStatementConnection By(string propertyId, IOperation operation)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By(string propertyId, IOperation operation, Connector connector)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By(IFilterInfo s2)
        {
            this._statements.Add(s2);
            return new FilterStatementConnection(this, s2);
        }

        public IFilterStatementConnection By<T1>(string propertyId, IOperation operation, T1 value)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, Connector connector)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void StartGroup()
        {
            throw new NotImplementedException();
        }

        public void Validate()
        {
        }
    }
}
