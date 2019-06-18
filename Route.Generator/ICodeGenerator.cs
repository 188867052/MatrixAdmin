namespace Route.Generator
{
    public interface ICodeGenerator
    {
        bool Generate(string projectName, string outPutFile);
    }
}