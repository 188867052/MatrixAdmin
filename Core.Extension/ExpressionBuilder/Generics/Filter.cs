using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Core.Extension.ExpressionBuilder.Builders;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;
using Core.Extension.ExpressionBuilder.Operations;

namespace Core.Extension.ExpressionBuilder.Generics
{
    /// <summary>
    /// Aggregates <see cref="FilterInfo{TPropertyType}" /> and build them into a LINQ expression.
    /// </summary>
    [Serializable]
    public class Filter<T> : IFilter, IXmlSerializable where T : class
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
        public Filter(IFilterInfo statement)
        {
            this._statements = new List<List<IFilterInfo>> { new List<IFilterInfo>() };
            this.By(statement);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{TClass}"/> class.
        /// Instantiates a new <see cref="Filter{TClass}" />.
        /// </summary>
        public Filter(Filter<T> f1, Filter<T> f2, Connector connector)
        {
            this._statements = new List<List<IFilterInfo>> { new List<IFilterInfo>() };
            IFilterInfo s1 = f1.CurrentStatementGroup.First();
            IFilterInfo s2 = f2.CurrentStatementGroup.First();
            if (connector == Connector.Or)
            {
                this.By(s1).Or.By(s2);
            }
            else
            {
                this.By(s1).And.By(s2);
            }
        }

        /// <summary>
        /// Group.
        /// </summary>
        public IFilter Group
        {
            get
            {
                this.StartGroup();
                return this;
            }
        }

        /// <summary>
        /// List of <see cref="IFilterInfo" /> groups that will be combined and built into a LINQ expression.
        /// </summary>
        public IEnumerable<IEnumerable<IFilterInfo>> Statements
        {
            get
            {
                return this._statements.ToArray();
            }
        }

        private List<IFilterInfo> CurrentStatementGroup
        {
            get
            {
                return this._statements.Last();
            }
        }

        /// <summary>
        /// Implicitly converts a <see cref="Filter{TClass}" /> into a <see cref="System.Linq.Expressions.Expression{Func{T, TResult}}" />.
        /// </summary>
        /// <param name="filter">filter.</param>
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

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// (To be used by <see cref="IOperation" /> that need no values).
        /// </summary>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <param name="connector">connector.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By(string propertyId, IOperation operation, Connector connector)
        {
            return this.By<string>(propertyId, operation, null, null, connector);
        }

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// (To be used by <see cref="IOperation" /> that need no values).
        /// </summary>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By(string propertyId, IOperation operation)
        {
            return this.By<string>(propertyId, operation, null, null, Connector.And);
        }

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// </summary>
        /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <param name="value">value.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value)
        {
            return this.By(propertyId, operation, value, default(TPropertyType));
        }

        public IFilterStatementConnection AddExistsFilter<TPropertyType>(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, int>> secondExpression, IOperation operation, int value)
        {
            string name = expression.ToString().Split('.')[1]
                + $"[{secondExpression.ToString().Split('.')[1]}]";
            IFilterInfo statement = new FilterInfo<int>(name, operation, value, default, Connector.And);
            this.CurrentStatementGroup.Add(statement);
            return new FilterStatementConnection(this, statement);
        }

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// </summary>
        /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <param name="value">value.</param>
        /// <param name="connector">connector.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, Connector connector)
        {
            return this.By(propertyId, operation, value, default, connector);
        }

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// </summary>
        /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <param name="value">value.</param>
        /// <param name="value2">value2.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2)
        {
            return this.By(propertyId, operation, value, value2, Connector.And);
        }

        public IFilterStatementConnection AddIntegerBetweenFilter(Expression<Func<T, int?>> expression, int min, int max)
        {
            return this.By(expression.GetPropertyName(), Operation.Between, min, max, Connector.And);
        }

        /// <summary>
        /// Adds a new <see cref="FilterInfo{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// </summary>
        /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Operation to be used.</param>
        /// <param name="value">value.</param>
        /// <param name="value2">value2.</param>
        /// <param name="connector">connector.</param>
        /// <returns>IFilterStatementConnection.</returns>
        public IFilterStatementConnection By<TPropertyType>(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector)
        {
            IFilterInfo statement = new FilterInfo<TPropertyType>(propertyId, operation, value, value2, connector);
            this.CurrentStatementGroup.Add(statement);
            return new FilterStatementConnection(this, statement);
        }

        public IFilterStatementConnection By(IFilterInfo statement)
        {
            this.CurrentStatementGroup.Add(statement);
            return new FilterStatementConnection(this, statement);
        }

        /// <summary>
        /// Starts a new group denoting that every subsequent filter statement should be grouped together (as if using a parenthesis).
        /// </summary>
        public void StartGroup()
        {
            if (this.CurrentStatementGroup.Any())
            {
                this._statements.Add(new List<IFilterInfo>());
            }
        }

        /// <summary>
        /// Removes all <see cref="FilterInfo{TPropertyType}" />, leaving the <see cref="Filter{TClass}" /> empty.
        /// </summary>
        public void Clear()
        {
            this._statements.Clear();
            this._statements.Add(new List<IFilterInfo>());
        }

        /// <summary>
        /// String representation of <see cref="Filter{TClass}" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = new StringBuilder();
            Connector lastConector = Connector.And;

            foreach (var statementGroup in this._statements)
            {
                if (this._statements.Count() > 1)
                {
                    result.Append("(");
                }

                var groupResult = new System.Text.StringBuilder();
                foreach (var statement in statementGroup)
                {
                    if (groupResult.Length > 0)
                    {
                        groupResult.Append(" " + lastConector + " ");
                    }

                    groupResult.Append(statement);
                    lastConector = statement.Connector;
                }

                result.Append(groupResult.ToString().Trim());
                if (this._statements.Count() > 1)
                {
                    result.Append(")");
                }
            }

            return result.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        ///  Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The System.Xml.XmlReader stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.Name.Equals("StatementsGroup") && reader.IsStartElement())
                {
                    this.StartGroup();
                }

                if (reader.Name.StartsWith("FilterStatementOf"))
                {
                    var type = reader.GetAttribute("Type");
                    var filterType = typeof(FilterInfo<>).MakeGenericType(Type.GetType(type));
                    var serializer = new XmlSerializer(filterType);
                    var statement = (IFilterInfo)serializer.Deserialize(reader);
                    this.CurrentStatementGroup.Add(statement);
                }
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The System.Xml.XmlWriter stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Type", typeof(T).AssemblyQualifiedName);
            writer.WriteStartElement("Statements");
            foreach (var statementsGroup in this._statements)
            {
                writer.WriteStartElement("StatementsGroup");
                foreach (var statement in statementsGroup)
                {
                    var serializer = new XmlSerializer(statement.GetType());
                    serializer.Serialize(writer, statement);
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public IFilterStatementConnection AddIntegerInArrayFilter<T1>(Expression<Func<T1, int?>> expression, int[] value)
        {
            return this.By(expression.GetPropertyName(), Operation.In, value, default(int[]));
        }
    }
}