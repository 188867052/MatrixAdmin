using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Route.Generator
{
    public abstract class OptionsCommandBase : CommandBase
    {
        protected OptionsCommandBase(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer)
            : base(logger, console)
        {
            this.Serializer = serializer;
        }

        [Option("-p <project>", Description = "The project name")]
        public string ProjectName { get; set; }

        [Option("-f <file>", Description = "The options file name")]
        public string OptionsFile { get; set; }

        [Option("-o <output>", Description = "The out put file name")]
        public string OutPutFile { get; set; }

        protected IGeneratorOptionsSerializer Serializer { get; }
    }
}