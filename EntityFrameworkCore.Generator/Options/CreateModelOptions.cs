namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Create model file generation options.
    /// </summary>
    /// <seealso cref="ModelOptionsBase" />
    public class CreateModelOptions : ModelOptionsBase
    {
        public CreateModelOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Create"))
        {
            this.Name = "{Entity.Name}CreateModel";
        }
    }
}