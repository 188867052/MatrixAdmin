using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Route.Generator
{
    [Command("generate", "gen")]
    public class GenerateCommand : OptionsCommandBase
    {
        private readonly ICodeGenerator _codeGenerator;

        public GenerateCommand(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer, ICodeGenerator codeGenerator)
            : base(logger, console, serializer)
        {
            this._codeGenerator = codeGenerator;
        }

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = this.WorkingDirectory ?? Environment.CurrentDirectory;
            var result = this._codeGenerator.Generate(workingDirectory);

            return result ? 0 : 1;
        }
    }
}