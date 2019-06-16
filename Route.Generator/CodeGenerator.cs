using System;
using System.IO;

namespace Route.Generator
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly RouteGenerator _modelGenerator;

        public CodeGenerator()
        {
            this._modelGenerator = new RouteGenerator();
        }

        public bool Generate(string workingDirectory)
        {
            var context = this._modelGenerator.GenerateCodeAsync(workingDirectory).Result;
            Console.WriteLine(context);
            string fullPath = Path.Combine(workingDirectory, "Routes.Generated.cs");
            File.WriteAllText(fullPath, context);
            return true;
        }
    }
}
