namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Read model options.
    /// </summary>
    /// <seealso cref="ModelOptionsBase" />
    public class ReadModelOptions : ModelOptionsBase
    {
        public ReadModelOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Read"))
        {
            this.Name = "{Entity.Name}ReadModel";
        }
    }
}