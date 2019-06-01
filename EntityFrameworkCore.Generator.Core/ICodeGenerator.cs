using EntityFrameworkCore.Generator.Core.Options;

namespace EntityFrameworkCore.Generator.Core
{
    public interface ICodeGenerator
    {
        bool Generate(GeneratorOptions options);
    }
}