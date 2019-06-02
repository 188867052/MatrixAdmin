namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// EntityFramework mapping class generation options.
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class MappingClassOptions : ClassOptionsBase
    {
        public MappingClassOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Mapping"))
        {
            this.Namespace = "{Project.Namespace}.Data.Mapping";
            this.Directory = @"{Project.Directory}\Data\Mapping";
        }
    }
}