namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Update model options.
    /// </summary>
    /// <seealso cref="ModelOptionsBase" />
    public class UpdateModelOptions : ModelOptionsBase
    {
        public UpdateModelOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Update"))
        {
            this.Name = "{Entity.Name}UpdateModel";
        }
    }
}