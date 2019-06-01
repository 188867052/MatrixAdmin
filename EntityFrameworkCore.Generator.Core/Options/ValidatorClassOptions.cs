﻿namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Validator class options.
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class ValidatorClassOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorClassOptions"/> class.
        /// </summary>
        public ValidatorClassOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Validator"))
        {
            this.Generate = false;
            this.Namespace = "{Project.Namespace}.Domain.Validation";
            this.Directory = @"{Project.Directory}\Domain\Validation";

            this.BaseClass = "AbstractValidator<{Model.Name}>";
            this.Name = "{Model.Name}Validator";
        }

        /// <summary>
        /// Gets or sets a value indicating whether this option is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> to generate; otherwise, <c>false</c>.
        /// </value>
        public bool Generate { get; set; }

        /// <summary>
        /// Gets or sets the validator class name template.
        /// </summary>
        /// <value>
        /// The validator class name template.
        /// </value>
        public string Name
        {
            get => this.GetProperty();
            set => this.SetProperty(value);
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
    }
}