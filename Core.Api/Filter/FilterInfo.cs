using System;
using System.Collections.Generic;
using Core.Api.ExpressionBuilder.Common;
using Core.Api.ExpressionBuilder.Interfaces;

namespace Core.Api.Filter
{
    public class FilterInfo<TPropertyType>: IFilterInfo
    {
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
        /// <summary>
        /// Instantiates a new <see cref="FilterInfo{TPropertyType}" />.
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="connector"></param>
        public FilterInfo(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector = default)
        {
            this.PropertyName = propertyId;
            this.Connector = connector;
            this.Operation = operation;
            this.SetValues(value, value2);
            //Validate();
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
    }
}
