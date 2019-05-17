using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Exceptions;
using Core.Extension.ExpressionBuilder.Helpers;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    /// <summary>
    /// Defines how a property should be filtered.
    /// </summary>
    /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
    [Serializable]
    public class FilterInfo<T, TCollection, TPropertyType> : IFilterInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterInfo{TPropertyType}"/> class.
        /// Instantiates a new <see cref="FilterInfo{TPropertyType}" />.
        /// </summary>
        public FilterInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterInfo{TPropertyType}"/> class.
        /// Instantiates a new <see cref="FilterInfo{TPropertyType}" />.
        /// </summary>
        /// <param name="propertyName">propertyId.</param>
        /// <param name="operation">operation.</param>
        /// <param name="value">value.</param>
        /// <param name="value2">value2.</param>
        /// <param name="connector">connector.</param>
        public FilterInfo(string propertyName, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector = default)
        {
            this.PropertyName = propertyName;
            this.Connector = connector;
            this.Operation = operation;
            this.SetValues(value, value2);
            this.Validate();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterInfo{TPropertyType}"/> class.
        /// Instantiates a new <see cref="FilterInfo{TPropertyType}" />.
        /// </summary>
        /// <param name="propertyName">propertyId.</param>
        /// <param name="operation">operation.</param>
        /// <param name="value">value.</param>
        /// <param name="value2">value2.</param>
        /// <param name="connector">connector.</param>
        public FilterInfo(Expression<Func<T, int>> secondExpression, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector = default)
        {
            this.PropertyName = secondExpression.GetPropertyName();
            this.Connector = connector;
            this.Operation = operation;
            this.SetValues(value, value2);
            this.Validate();
        }

        public FilterInfo(string propertyId, IOperation operation, TPropertyType value)
        {
            this.PropertyName = propertyId;
            this.Operation = operation;
            this.SetValues(value);
            this.Validate();
        }

        public FilterInfo(Expression<Func<T, ICollection<TCollection>>> expression, Expression<Func<TCollection, TPropertyType>> secondExpression, IOperation operation, TPropertyType value)
        {
            string name = expression.ToString().Split('.')[1] + $"[{secondExpression.ToString().Split('.')[1]}]";
            this.PropertyName = name;
            this.Operation = operation;
            this.SetValues(value);
            this.Validate();
        }

        /// <summary>
        /// Establishes how this filter statement will connect to the next one.
        /// </summary>
        public Connector Connector { get; set; }

        /// <summary>
        /// Property identifier conventionalized by for the Expression Builder.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Express the interaction between the property and the constant value defined in this filter statement.
        /// </summary>
        public IOperation Operation { get; set; }

        /// <summary>
        /// Constant value that will interact with the property defined in this filter statement.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Constant value that will interact with the property defined in this filter statement when the operation demands a second value to compare to.
        /// </summary>
        public object Value2 { get; set; }

        public bool IsFilterEnable => true;

        public IEnumerable<IFilterInfo> FilterInfos => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        /// <summary>
        /// Validates the FilterStatement regarding the number of provided values and supported operations.
        /// </summary>
        public void Validate()
        {
            var helper = new OperationHelper();
            //this.ValidateNumberOfValues();
            this.ValidateSupportedOperations(helper);
        }

        private void ValidateNumberOfValues()
        {
            var numberOfValues = this.Operation.NumberOfValues;
            var failsForSingleValue = numberOfValues == 1 && !Equals(this.Value2, default(TPropertyType));
            var failsForNoValueAtAll = numberOfValues == 0 && (!Equals(this.Value, default(TPropertyType)) || !Equals(this.Value2, default(TPropertyType)));

            if (failsForSingleValue || failsForNoValueAtAll)
            {
                throw new WrongNumberOfValuesException(this.Operation);
            }
        }

        private void ValidateSupportedOperations(OperationHelper helper)
        {
            if (typeof(TPropertyType) == typeof(object))
            {
                // TODO: Issue regarding the TPropertyType that comes from the UI always as 'Object'
                System.Diagnostics.Debug.WriteLine("WARN: Not able to check if the operation is supported or not.");
                return;
            }

            var supportedOperations = helper.SupportedOperations(typeof(TPropertyType));

            //if (!supportedOperations.Contains(this.Operation))
            //{
            //    throw new UnsupportedOperationException(this.Operation, typeof(TPropertyType).Name);
            //}
        }

        private void SetValues(TPropertyType value, TPropertyType value2)
        {
            if (typeof(TPropertyType).IsArray)
            {
                if (!this.Operation.SupportsLists)
                {
                    throw new ArgumentException("It seems the chosen operation does not support arrays as parameters.");
                }

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(typeof(TPropertyType).GetElementType());
                this.Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
                this.Value2 = value2 != null ? Activator.CreateInstance(constructedListType, value2) : null;
            }
            else
            {
                this.Value = value;
                this.Value2 = value2;
            }
        }

        private void SetValues(TPropertyType value)
        {
            if (typeof(TPropertyType).IsArray)
            {
                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(typeof(TPropertyType).GetElementType());
                this.Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
            }
            else
            {
                this.Value = value;
            }
        }
    }
}