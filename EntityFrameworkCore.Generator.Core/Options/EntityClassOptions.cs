using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Core.Options
{
    /// <summary>
    /// EntityFramework entity class generation options.
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class EntityClassOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityClassOptions"/> class.
        /// </summary>
        public EntityClassOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Entity"))
        {
            this.Namespace = "{Project.Namespace}.Data.Entities";
            this.Directory = @"{Project.Directory}\Data\Entities";

            this.RelationshipNaming = RelationshipNaming.Plural;
            this.EntityNaming = EntityNaming.Singular;
            this.PrefixWithSchemaName = false;
        }

        /// <summary>
        /// Gets or sets the base class to inherit from.
        /// </summary>
        /// <value>
        /// The base class.
        /// </value>
        public string BaseClass
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the entity class naming strategy.
        /// </summary>
        /// <value>
        /// The entity class naming strategy.
        /// </value>
        [DefaultValue(EntityNaming.Singular)]
        public EntityNaming EntityNaming { get; set; }

        /// <summary>
        /// Gets or sets the relationship property naming strategy.
        /// </summary>
        /// <value>
        /// The relationship property naming strategy.
        /// </value>
        [DefaultValue(RelationshipNaming.Plural)]
        public RelationshipNaming RelationshipNaming { get; set; }

        /// <summary>
        /// If true prefix classname with schema name to prevent naming conflicts.
        /// </summary>
        [DefaultValue(false)]
        public bool PrefixWithSchemaName { get; set; }
    }
}