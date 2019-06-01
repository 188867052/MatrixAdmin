using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using DbUp;
using DbUp.Engine.Output;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace FluentCommand.SqlServer.Tests
{
    public class DatabaseFixture : IUpgradeLog, IDisposable
    {
        private readonly StringBuilder _buffer;
        private readonly StringWriter _logger;

        public DatabaseFixture()
        {
            this._buffer = new StringBuilder();
            this._logger = new StringWriter(this._buffer);

            this.ResolveConnectionString();

            this.CreateDatabase();
        }

        public string ConnectionString { get; set; }

        public string ConnectionName { get; set; } = "Tracker";

        public void Report(ITestOutputHelper output)
        {
            if (this._buffer.Length == 0)
            {
                return;
            }

            this._logger.Flush();
            output.WriteLine(this._logger.ToString());

            // reset logger
            this._buffer.Clear();
        }

        public void Dispose()
        {
        }

        public void WriteInformation(string format, params object[] args)
        {
            this._logger.Write("INFO : ");
            this._logger.WriteLine(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            this._logger.Write("ERROR: ");
            this._logger.WriteLine(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            this._logger.Write("WARN : ");
            this._logger.WriteLine(format, args);
        }

        private void CreateDatabase()
        {
            EnsureDatabase.For
                .SqlDatabase(this.ConnectionString, this);

            var upgradeEngine = DeployChanges.To
                    .SqlDatabase(this.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogTo(this)
                    .Build();

            var result = upgradeEngine.PerformUpgrade();

            if (result.Successful)
            {
                return;
            }

            this._logger.WriteLine($"Exception: '{result.Error}'");

            throw result.Error;
        }

        private void ResolveConnectionString()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString(this.ConnectionName);

            this.ConnectionString = connectionString;
        }
    }
}