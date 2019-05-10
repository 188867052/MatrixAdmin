using System;

namespace Core.Api.ExpressionBuilder.Exceptions
{
    /// <summary>
    /// Represents an attempt to instantiate an operation that was not loaded.
    /// </summary>
    [Serializable]
    public class OperationNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationNotFoundException" /> class.
        /// </summary>
        /// <param name="operationName">Name of the operation that was intended to be instantiated.</param>
        public OperationNotFoundException(string operationName)
        {
            this.OperationName = operationName;
        }

        /// <summary>
        /// Name of the operation that was intended to be instantiated.
        /// </summary>
        public string OperationName { get; }

        /// <inheritdoc />
        public override string Message
        {
            get
            {
                return string.Format("Sorry, the operation '{0}' was not found.", this.OperationName);
            }
        }
    }
}