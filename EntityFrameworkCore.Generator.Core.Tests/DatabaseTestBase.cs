using System;
using System.Data.SqlClient;
using Xunit;
using Xunit.Abstractions;

namespace FluentCommand.SqlServer.Tests
{
    [Collection(DatabaseCollection.CollectionName)]
    public abstract class DatabaseTestBase : IDisposable
    {
        protected DatabaseTestBase(ITestOutputHelper output, DatabaseFixture databaseFixture)
        {
            this.Output = output;
            this.Database = databaseFixture;
        }

        public ITestOutputHelper Output { get; }

        public DatabaseFixture Database { get; }

        public void Dispose()
        {
            this.Database?.Report(this.Output);
        }
    }
}