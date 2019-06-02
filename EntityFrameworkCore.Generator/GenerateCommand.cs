using System;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
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

        [Option("-p <Provider>", Description = "Database provider to reverse engineer")]
        public DatabaseProviders? Provider { get; set; }

        [Option("-c <ConnectionString>", Description = "Database connection string to reverse engineer")]
        public string ConnectionString { get; set; }

        [Option("--extensions", Description = "Include query extensions in generation")]
        public bool? Extensions { get; set; }

        [Option("--models", Description = "Include view models in generation")]
        public bool? Models { get; set; }

        [Option("--mapper", Description = "Include object mapper in generation")]
        public bool? Mapper { get; set; }

        [Option("--validator", Description = "Include model validation in generation")]
        public bool? Validator { get; set; }

        protected override int OnExecute(CommandLineApplication application)
        {
            var workingDirectory = this.WorkingDirectory ?? Environment.CurrentDirectory;
            var optionsFile = this.OptionsFile ?? GeneratorOptionsSerializer.OptionsFileName;

            var options = this.Serializer.Load(workingDirectory, optionsFile);
            if (options == null)
            {
                this.Logger.LogInformation("Using default options");
                options = new GeneratorOptions();
            }

            // override options
            if (this.ConnectionString.HasValue())
            {
                options.Database.ConnectionString = this.ConnectionString;
            }

            if (this.Provider.HasValue)
            {
                options.Database.Provider = this.Provider.Value;
            }

            if (this.Extensions.HasValue)
            {
                options.Data.Query.Generate = this.Extensions.Value;
            }

            if (this.Models.HasValue)
            {
                options.Model.Read.Generate = this.Models.Value;
                options.Model.Create.Generate = this.Models.Value;
                options.Model.Update.Generate = this.Models.Value;
            }

            if (this.Mapper.HasValue)
            {
                options.Model.Mapper.Generate = this.Mapper.Value;
            }

            if (this.Validator.HasValue)
            {
                options.Model.Validator.Generate = this.Validator.Value;
            }

            var result = this._codeGenerator.Generate(options);

            return result ? 0 : 1;
        }
    }
}