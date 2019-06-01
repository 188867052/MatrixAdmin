namespace EntityFrameworkCore.Generator.Core.Options
{
    /// <summary>
    /// Query extensions options.
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class QueryExtensionOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryExtensionOptions"/> class.
        /// </summary>
        public QueryExtensionOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Query"))
        {
            this.Namespace = "{Project.Namespace}.Data.Queries";
            this.Directory = @"{Project.Directory}\Data\Queries";

            this.Generate = false;
            this.IndexPrefix = "By";
            this.UniquePrefix = "GetBy";
        }

        /// <summary>
        /// Gets or sets a value indicating whether this option is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> to generate; otherwise, <c>false</c>.
        /// </value>
        public bool Generate { get; set; }

        /// <summary>
        /// Gets or sets the prefix of query method names.
        /// </summary>
        /// <value>
        /// The prefix of query method names.
        /// </value>
        public string IndexPrefix
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the prefix of unique query method names.
        /// </summary>
        /// <value>
        /// The prefix of unique query method names.
        /// </value>
        public string UniquePrefix
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }
    }
}