namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Project options.
    /// </summary>
    public class ProjectOptions : OptionsBase
    {
        public ProjectOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Project"))
        {
            this.Namespace = "{Database.Name}";
            this.Directory = @".\";
        }

        /// <summary>
        /// Gets or sets the project root namespace.
        /// </summary>
        /// <value>
        /// The project root namespace.
        /// </value>
        public string Namespace
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the project directory.
        /// </summary>
        /// <value>
        /// The project directory.
        /// </value>
        public string Directory
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }
    }
}