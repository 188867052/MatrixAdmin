using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Top level generator configuration options.
    /// </summary>
    public class GeneratorOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorOptions"/> class.
        /// </summary>
        public GeneratorOptions()
        {
            this.Variables = new VariableDictionary();

            this.Project = new ProjectOptions(this.Variables, null);
            this.Database = new DatabaseOptions(this.Variables, null);
            this.Data = new DataOptions(this.Variables, null);
            this.Model = new ModelOptions(this.Variables, null);
        }

        [YamlIgnore]
        public VariableDictionary Variables { get; }

        /// <summary>
        /// Gets or sets the project options.
        /// </summary>
        /// <value>
        /// The project level options.
        /// </value>
        public ProjectOptions Project { get; set; }

        /// <summary>
        /// Gets or sets the options for reverse engineer the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public DatabaseOptions Database { get; set; }

        /// <summary>
        /// Gets or sets the EntityFramework configuration options.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public DataOptions Data { get; set; }

        /// <summary>
        /// Gets or sets the domain view model options.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public ModelOptions Model { get; set; }
    }
}