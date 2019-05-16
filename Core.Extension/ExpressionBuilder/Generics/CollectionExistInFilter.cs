using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class CollectionExistInFilter<T, TPropertyType> : IFilter
    {
        private readonly List<IFilterInfo> _statements;

        public CollectionExistInFilter(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, int>> secondExpression, IOperation operation, int value)
        {
            IFilterInfo statement = new FilterInfo<T, TPropertyType, int>(expression, secondExpression, operation, value);
            this._statements = new List<IFilterInfo>();
            _statements.Add(statement);
        }

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; } = Operations.Operation.Between;

        public object Value { get; set; }

        public object Value2 { get; set; }

        public IFilter Group => throw new NotImplementedException();

        public IEnumerable<IEnumerable<IFilterInfo>> Statements => throw new NotImplementedException();

        public IEnumerable<IFilterInfo> FilterInfos => this._statements;

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
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<T1>(string propertyId, IOperation operation, T1 value)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType1>(string propertyId, IOperation operation, TPropertyType1 value, Connector connector)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType1>(string propertyId, IOperation operation, TPropertyType1 value, TPropertyType1 value2)
        {
            throw new NotImplementedException();
        }

        public IFilterStatementConnection By<TPropertyType1>(string propertyId, IOperation operation, TPropertyType1 value, TPropertyType1 value2, Connector connector)
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

        //private List<IFilterInfo> CurrentStatementGroup
        //{
        //    get
        //    {
        //        return this._statements;
        //    }
        //}
    }
}
