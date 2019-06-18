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
            Console.WriteLine($"this.ProjectName: { this.ProjectName}");
            Console.WriteLine($"this.OutPutFile: { this.OutPutFile}");
            var result = this._codeGenerator.Generate(this.ProjectName, this.OutPutFile);

            return result ? 0 : 1;
        }
    }
}