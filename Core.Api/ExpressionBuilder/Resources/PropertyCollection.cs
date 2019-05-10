using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Core.Api.ExpressionBuilder.Interfaces;

namespace Core.Api.ExpressionBuilder.Resources
{
    /// <summary>
    /// Collection of <see cref="Property" />.
    /// </summary>
    public class PropertyCollection : IPropertyCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// Instantiates a new <see cref="PropertyCollection" />.
        /// </summary>
        /// <param name="type"></param>
        public PropertyCollection(Type type)
        {
            this.Type = type;
            this._visitedTypes = new HashSet<Type>();
            this.Properties = this.LoadProperties(this.Type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// Instantiates a new <see cref="PropertyCollection" />.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="resourceManager"></param>
        public PropertyCollection(Type type, ResourceManager resourceManager) : this(type)
        {
            this.LoadProperties(resourceManager);
        }

        /// <summary>
        /// Type from which the properties are loaded.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// ResourceManager which the properties descriptions should be gotten from.
        /// </summary>
        public ResourceManager ResourceManager { get; private set; }

        private readonly HashSet<Type> _visitedTypes;

        private List<Property> Properties { get; }

        /// <summary>
        /// Gets the number of <see cref="Property" /> contained in the <see cref="PropertyCollection" />.
        /// </summary>
        public int Count
        {
            get { return this.Properties.Count; }
        }

        /// <summary>
        ///
        /// </summary>
        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Retrieves a property based on its Id.
        /// </summary>
        /// <param name="propertyId">Property conventionalized <see cref="Property.Id" />.</param>
        /// <returns></returns>
        public Property this[string propertyId]
        {
            get { return this.Properties.FirstOrDefault(p => p.Id.Equals(propertyId)); }
        }

        /// <summary>
        /// Loads the properties names from the specified ResourceManager.
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <returns></returns>
        public List<Property> LoadProperties(ResourceManager resourceManager)
        {
            this.ResourceManager = resourceManager;
            foreach (Property property in this.Properties)
            {
                property.Name = resourceManager.GetString(this.GetPropertyResourceName(property.Id)) ?? property.Name;
            }

            return this.Properties;
        }

        private string GetPropertyResourceName(string propertyConventionName)
        {
            return propertyConventionName
                                        .Replace("[", "_")
                                        .Replace("]", "_")
                                        .Replace(".", "_");
        }

        private List<Property> LoadProperties(Type type)
        {
            var list = new List<Property>();
            if (this._visitedTypes.Contains(type))
            {
                return list;
            }

            this._visitedTypes.Add(type);

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            MemberInfo[] members = type.GetFields(bindingFlags).Cast<MemberInfo>()
                                    .Concat(type.GetProperties(bindingFlags)).ToArray();
            foreach (var member in members)
            {
                list.AddRange(this.GetProperties(member));
            }

            return list;
        }

        private IEnumerable<Property> GetProperties(MemberInfo member)
        {
            var memberType = this.GetMemberType(member);

            if (memberType.IsValueType || memberType == typeof(string))
            {
                return new List<Property> { new Property(member.Name, member.Name, member) };
            }

            if (memberType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(memberType))
            {
                return this.LoadProperties(memberType.GetGenericArguments()[0])
                        .Select(p => new Property(member.Name + "[" + p.Id + "]", p.Name, p.Info));
            }

            return this.LoadProperties(memberType)
                    .Select(p => new Property(member.Name + "." + p.Id, p.Name, p.Info));
        }

        private Type GetMemberType(MemberInfo member)
        {
            return member.MemberType == MemberTypes.Property ? (member as PropertyInfo).PropertyType : (member as FieldInfo).FieldType;
        }

        /// <summary>
        /// Copies the elements of the <see cref="PropertyCollection" /> to an System.Array,
        /// starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination of the elements copied
        /// from System.Collections.ICollection. The System.Array must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            this.Properties.CopyTo((Property[])array, index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this.Properties.GetEnumerator();
        }

        /// <summary>
        /// Converts the collection into a list.
        /// </summary>
        /// <returns></returns>
        public IList<Property> ToList()
        {
            Property[] properties = new Property[this.Properties.Count];
            this.CopyTo(properties, 0);
            return properties;
        }
    }
}