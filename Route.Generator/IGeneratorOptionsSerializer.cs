namespace Route.Generator
{
    /// <summary>
    /// <c>interface</c> for serialization and deserialization of <see cref="GeneratorOptions"/> class.
    /// </summary>
    public interface IGeneratorOptionsSerializer
    {
        string Save(string directory);
    }
}