using System.IO;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class ContextParser
    {
        private readonly ILogger _logger;

        public ContextParser(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<ContextParser>();
        }

        public ParsedContext ParseFile(string contextFile)
        {
            if (string.IsNullOrEmpty(contextFile) || !File.Exists(contextFile))
            {
                return null;
            }

            this._logger.LogDebug(
                "Parsing Context File: '{0}'",
                Path.GetFileName(contextFile));

            var code = File.ReadAllText(contextFile);
            return this.ParseCode(code);
        }

        public ParsedContext ParseCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();

            var visitor = new ContextVisitor();
            visitor.Visit(root);

            var parsedContext = visitor.ParsedContext;
            if (parsedContext == null)
            {
                return null;
            }

            this._logger.LogDebug(
                "Parsed Context Class: {0}; Entities: {1}",
                parsedContext.ContextClass,
                parsedContext.Properties.Count);

            return parsedContext;
        }
    }
}
