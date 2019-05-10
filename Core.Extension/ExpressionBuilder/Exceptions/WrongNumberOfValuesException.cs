using System;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Exceptions
{
    /// <summary>
    /// Represents an attempt to use an operation providing the wrong number of values.
    /// </summary>
    [Serializable]
    public class WrongNumberOfValuesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongNumberOfValuesException" /> class.
        /// </summary>
        /// <param name="operation">Operation used.</param>
        public WrongNumberOfValuesException(IOperation operation)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Gets the <see cref="Operation" /> attempted to be used.
        /// </summary>
        public IOperation Operation { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("The operation '{0}' admits exactly '{1}' values (not more neither less than this).", this.Operation.Name, this.Operation.NumberOfValues);
            }
        }
    }
}