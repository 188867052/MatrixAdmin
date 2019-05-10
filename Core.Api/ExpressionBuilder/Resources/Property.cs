using System.Reflection;

namespace Core.Api.ExpressionBuilder.Resources
{
    /// <summary>
    /// Provides information on the property to the Expression Builder.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <param name="info">info</param>
        internal Property(string id, string name, MemberInfo info)
        {
            this.Id = id;
            this.Name = name;
            this.Info = info;
        }

        /// <summary>
        /// Property identifier conventionalized by the Expression Builder.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Property name obtained from a ResourceManager, or the property's original name (in the absence of a ResourceManager correspondent value).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property metadata.
        /// </summary>
        public MemberInfo Info { get; private set; }

        public System.Type MemberType
        {
            get
            {
                return this.Info.MemberType == MemberTypes.Property ? (this.Info as PropertyInfo).PropertyType : (this.Info as FieldInfo).FieldType;
            }
        }

        /// <summary>
        /// String representation of <see cref="Property" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Id);
        }
    }
}