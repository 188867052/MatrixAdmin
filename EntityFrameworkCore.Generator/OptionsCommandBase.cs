using System;
using EntityFrameworkCore.Generator.Core;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    public abstract class OptionsCommandBase : CommandBase
    {
        protected OptionsCommandBase(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer)
            : base(logger, console)
        {
            this.Serializer = serializer;
        }

        [Option("-d <directory>", Description = "The root working directory")]
        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        [Option("-f <file>", Description = "The options file name")]
        public string OptionsFile { get; set; } = GeneratorOptionsSerializer.OptionsFileName;

        protected IGeneratorOptionsSerializer Serializer { get; }
    }
}