using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Builders;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    /// <summary>
    /// Aggregates <see cref="FilterInfo{TPropertyType}" /> and build them into a LINQ expression.
    /// </summary>
    [Serializable]
    public class Filter<T> : IFilter where T : class
    {
        private readonly List<List<IFilterInfo>> _statements;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{TClass}"/> class.
        /// Instantiates a new <see cref="Filter{TClass}" />.
        /// </summary>
        public Filter()
        {
            this._statements = new List<List<IFilterInfo>> { new List<IFilterInfo>() };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{TClass}"/> class.
        /// Instantiates a new <see cref="Filter{TClass}" />.
        /// </summary>
        public Filter(IFilterInfo f1, IFilterInfo f2, Connector connector)
        {
            this._statements = new List<List<IFilterInfo>> { new List<IFilterInfo>() };
            this.CurrentStatementGroup.Add(f1);
            var connection = new FilterStatementConnection(this, f1);
            if (connector == Connector.Or)
            {
                connection.Or.By(f2);
            }
            else
            {
                connection.And.By(f2);
            }
        }

        /// <summary>
        /// List of <see cref="IFilterInfo" /> groups that will be combined and built into a LINQ expression.
        /// </summary>
        public IEnumerable<IEnumerable<IFilterInfo>> Statements => this._statements.ToArray();

        private List<IFilterInfo> CurrentStatementGroup
        {
            get
            {
                return this._statements.Last();
            }
        }

        public static implicit operator Expression<Func<T, bool>>(Filter<T> filter)
        {
            var builder = new FilterBuilder();
            var expression = builder.GetExpression<T>(filter);
            return expression;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Filter{TClass}" /> into a <see cref="Func{TClass, TResult}" />.
        /// </summary>
        /// <param name="filter">filter.</param>
        public static implicit operator Func<T, bool>(Filter<T> filter)
        {
            var builder = new FilterBuilder();
            var expression = builder.GetExpression<T>(filter).Compile();
            return expression;
        }

        public void AddSimpleFilter(IFilterInfo f1)
        {
            if (f1.IsFilterEnable)
            {
                this.CurrentStatementGroup.Add(f1);
            }
        }

        public void AddExistFilter(IFilterInfo f1)
        {
            if (f1.IsFilterEnable)
            {
                this._statements.Add(f1.FilterInfos.ToList());
            }
        }

        public void AddFilter(IFilter f1)
        {
            this._statements.Add(f1.FilterInfos.ToList());
        }

        public IEnumerable<IFilterInfo> FilterInfos
        {
            get
            {
                return this._statements.FirstOrDefault();
            }
        }

        // by2
        public IFilterStatementConnection By(IFilterInfo statement)
        {
            this.CurrentStatementGroup.Add(statement);
            return new FilterStatementConnection(this, statement);
        }
    }
}