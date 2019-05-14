using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
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
    public class FilterInfo<TPropertyType> : IFilterInfo
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
        /// <param name="propertyId">propertyId.</param>
        /// <param name="operation">operation.</param>
        /// <param name="value">value.</param>
        /// <param name="value2">value2.</param>
        /// <param name="connector">connector.</param>
        public FilterInfo(string propertyId, IOperation operation, TPropertyType value, TPropertyType value2, Connector connector = default)
        {
            this.PropertyName = propertyId;
            this.Connector = connector;
            this.Operation = operation;
            this.SetValues(value, value2);
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

        /// <summary>
        /// String representation of <see cref="FilterInfo{TPropertyType}" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (this.Operation.NumberOfValues)
            {
                case 0:
                    return string.Format("{0} {1}", this.PropertyName, this.Operation);

                case 2:
                    return string.Format("{0} {1} {2} And {3}", this.PropertyName, this.Operation, this.Value, this.Value2);

                default:
                    return string.Format("{0} {1} {2}", this.PropertyName, this.Operation, this.Value);
            }
        }

        /// <summary>
        /// GetSchema.
        /// </summary>
        /// <returns>XmlSchema.</returns>
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
            reader.Read();
            this.PropertyName = reader.ReadElementContentAsString();
            this.Operation = Operations.Operation.ByName(reader.ReadElementContentAsString());
            if (typeof(TPropertyType).IsEnum)
            {
                this.Value = Enum.Parse(typeof(TPropertyType), reader.ReadElementContentAsString());
            }
            else
            {
                this.Value = Convert.ChangeType(reader.ReadElementContentAsString(), typeof(TPropertyType));
            }

            this.Connector = (Connector)Enum.Parse(typeof(Connector), reader.ReadElementContentAsString());
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The System.Xml.XmlWriter stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            var type = this.Value.GetType();
            writer.WriteAttributeString("Type", type.AssemblyQualifiedName);
            writer.WriteElementString("PropertyId", this.PropertyName);
            writer.WriteElementString("Operation", this.Operation.Name);
            writer.WriteElementString("Value", this.Value.ToString());
            writer.WriteElementString("Connector", this.Connector.ToString("d"));
        }

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

            if (!supportedOperations.Contains(this.Operation))
            {
                throw new UnsupportedOperationException(this.Operation, typeof(TPropertyType).Name);
            }
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