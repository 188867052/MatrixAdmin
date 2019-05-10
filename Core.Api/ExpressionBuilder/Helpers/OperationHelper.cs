using System;
using System.Collections.Generic;
using System.Linq;
using Core.Api.ExpressionBuilder.Common;
using Core.Api.ExpressionBuilder.Configuration;
using Core.Api.ExpressionBuilder.Exceptions;
using Core.Api.ExpressionBuilder.Interfaces;

namespace Core.Api.ExpressionBuilder.Helpers
{
    /// <summary>
    /// Useful methods regarding <seealso cref="IOperation"></seealso>.
    /// </summary>
    public class OperationHelper : IOperationHelper
    {
        private static HashSet<IOperation> _operations;

        private readonly Settings _settings;

        private readonly Dictionary<TypeGroup, HashSet<Type>> typeGroups;

        static OperationHelper()
        {
            LoadDefaultOperations();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationHelper"/> class.
        /// Instantiates a new OperationHelper.
        /// </summary>
        public OperationHelper()
        {
            this._settings = new Settings();
            this.typeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>), typeof(string) } }
            };
        }

        /// <summary>
        /// List of all operations loaded so far.
        /// </summary>
        public IEnumerable<IOperation> Operations => _operations.ToArray();

        /// <summary>
        /// Loads the default operations overwriting any previous changes to the <see cref="Operations"></see> list.
        /// </summary>
        public static void LoadDefaultOperations()
        {
            var @interface = typeof(IOperation);
            var operationsFound = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.DefinedTypes.Any(t => t.Namespace == "ExpressionBuilder.Operations"))
                .SelectMany(s => s.GetTypes())
                .Where(p => @interface.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .Select(t => (IOperation)Activator.CreateInstance(t));
            _operations = new HashSet<IOperation>(operationsFound, new OperationEqualityComparer());
        }

        /// <summary>
        /// Instantiates an IOperation given its name.
        /// </summary>
        /// <param name="operationName">Name of the operation to be instantiated.</param>
        /// <returns></returns>
        public IOperation GetOperationByName(string operationName)
        {
            var operation = this.Operations.SingleOrDefault(o => o.Name == operationName && o.Active);

            if (operation == null)
            {
                throw new OperationNotFoundException(operationName);
            }

            return operation;
        }

        /// <summary>
        /// Loads a list of custom operations into the <see cref="Operations"></see> list.
        /// </summary>
        /// <param name="operations">List of operations to load.</param>
        public void LoadOperations(List<IOperation> operations)
        {
            this.LoadOperations(operations, false);
        }

        /// <summary>
        /// Loads a list of custom operations into the <see cref="Operations"></see> list.
        /// </summary>
        /// <param name="operations">List of operations to load.</param>
        /// <param name="overloadExisting">Specifies that any matching pre-existing operations should be replaced by the ones from the list. (Useful to overwrite the default operations).</param>
        public void LoadOperations(List<IOperation> operations, bool overloadExisting)
        {
            foreach (var operation in operations)
            {
                this.DeactivateOperation(operation.Name, overloadExisting);
                _operations.Add(operation);
            }
        }

        /// <summary>
        /// Retrieves a list of <see cref="IOperation"></see> supported by a type.
        /// </summary>
        /// <param name="type">Type for which supported operations should be retrieved.</param>
        /// <returns></returns>
        public HashSet<IOperation> SupportedOperations(Type type)
        {
            this.GetCustomSupportedTypes();
            return this.GetSupportedOperations(type);
        }

        private void GetCustomSupportedTypes()
        {
            foreach (var supportedType in this._settings.SupportedTypes)
            {
                if (supportedType.Type != null)
                {
                    this.typeGroups[supportedType.TypeGroup].Add(supportedType.Type);
                }
            }
        }

        private HashSet<IOperation> GetSupportedOperations(Type type)
        {
            var underlyingNullableType = Nullable.GetUnderlyingType(type);
            var typeName = (underlyingNullableType ?? type).Name;

            var supportedOperations = new List<IOperation>();
            if (type.IsArray)
            {
                typeName = type.GetElementType().Name;
                supportedOperations.AddRange(this.Operations.Where(o => o.SupportsLists && o.Active));
            }

            var typeGroup = TypeGroup.Default;
            if (this.typeGroups.Any(i => i.Value.Any(v => v.Name == typeName)))
            {
                typeGroup = this.typeGroups.FirstOrDefault(i => i.Value.Any(v => v.Name == typeName)).Key;
            }

            supportedOperations.AddRange(this.Operations.Where(o => o.TypeGroup.HasFlag(typeGroup) && !o.SupportsLists && o.Active));

            if (underlyingNullableType != null)
            {
                supportedOperations.AddRange(this.Operations.Where(o => o.TypeGroup.HasFlag(TypeGroup.Nullable) && !o.SupportsLists && o.Active));
            }

            return new HashSet<IOperation>(supportedOperations);
        }

        private void DeactivateOperation(string operationName, bool overloadExisting)
        {
            if (!overloadExisting)
            {
                return;
            }

            var op = _operations.FirstOrDefault(o => string.Compare(o.Name, operationName, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (op != null)
            {
                op.Active = false;
            }
        }
    }
}