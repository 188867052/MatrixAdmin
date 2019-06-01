using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Core.Options
{
    /// <summary>
    /// Base class for Class generation.
    /// </summary>
    public abstract class ClassOptionsBase : OptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassOptionsBase"/> class.
        /// </summary>
        protected ClassOptionsBase(VariableDictionary variables, string prefix)
            : base(variables, prefix)
        {
            this.Namespace = "{Project.Namespace}";
            this.Directory = @"{Project.Directory}\";
            this.Document = false;
        }

        /// <summary>
        /// Gets or sets the class namespace.
        /// </summary>
        /// <value>
        /// The class namespace.
        /// </value>
        public string Namespace
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// Gets or sets the output directory.  Default is the current working directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string Directory
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to create xml documentation.
        /// </summary>
        /// <value>
        ///   <c>true</c> to create xml documentation; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool Document { get; set; }
    }
}