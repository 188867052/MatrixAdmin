using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Helpers;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    /// <summary>
    /// Defines how a property should be filtered.
    /// </summary>
    /// <typeparam name="TPropertyType">TPropertyType.</typeparam>
    [Serializable]
    public class FilterInfo<TPropertyType> : IFilterInfo
    {
        public FilterInfo(string propertyId, IOperation operation, object value)
        {
            this.PropertyName = propertyId;
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

        public IEnumerable<IFilterInfo> FilterInfos { get; set; }

        public Expression Expression { get; set; }

        bool IFilterInfo.IsFilterEnable { get; set; }

        /// <summary>
        /// Validates the FilterStatement regarding the number of provided values and supported operations.
        /// </summary>
        public void Validate()
        {
            var helper = new OperationHelper();
        }

        private void SetValues(object value, object value2)
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

        private void SetValues(object value)
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