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

        public bool Generate(string projectName, string outPutFile)
        {
            var context = this._modelGenerator.GenerateCodeAsync(projectName).Result;
            Console.WriteLine(context);
            if (string.IsNullOrEmpty(outPutFile))
            {
                outPutFile = "Routes.Generated.cs";
            }

            if (!outPutFile.EndsWith(".cs"))
            {
                outPutFile += ".cs";
            }

            string fullPath = Path.Combine(Environment.CurrentDirectory, outPutFile);
            File.WriteAllText(fullPath, context);
            return true;
        }
    }
}
