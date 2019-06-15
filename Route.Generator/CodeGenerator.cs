using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Route.Generator
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly ILogger _logger;
        private readonly ModelGenerator _modelGenerator;

        public CodeGenerator(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<CodeGenerator>();
            this._modelGenerator = new ModelGenerator(loggerFactory);
        }

        public bool Generate(string workingDirectory)
        {
            var context = this._modelGenerator.GenerateCode();
            Console.WriteLine(context);
            string fullPath = Path.Combine(workingDirectory, "Routes.Generated.cs");
            File.WriteAllText(fullPath, context);
            return true;
        }
    }
}
