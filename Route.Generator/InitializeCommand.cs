using System;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Route.Generator
{
    [Command("initialize", "init")]
    public class InitializeCommand : OptionsCommandBase
    {
        public InitializeCommand(ILoggerFactory logger, IConsole console, IGeneratorOptionsSerializer serializer)
            : base(logger, console, serializer)
        {
        }

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = this.WorkingDirectory ?? Environment.CurrentDirectory;
            this.Logger.LogInformation($"工作目录:{workingDirectory}");
            this.Logger.LogInformation($" Environment.CurrentDirectory:{Environment.CurrentDirectory}");
            if (application.Arguments != null)
            {
                this.Logger.LogInformation(JsonConvert.SerializeObject(application.Arguments.Select(o => o.Value)));
            }

            if (!Directory.Exists(workingDirectory))
            {
                this.Logger.LogTrace($"Creating directory: {workingDirectory}");
                Directory.CreateDirectory(workingDirectory);
            }

            var optionsFile = this.OptionsFile;

            this.Logger.LogTrace($"optionsFile: {optionsFile}");
            this.Logger.LogTrace($"WorkingDirectory: {this.WorkingDirectory}");

            this.Serializer.Save(workingDirectory);

            return 0;
        }
    }
}