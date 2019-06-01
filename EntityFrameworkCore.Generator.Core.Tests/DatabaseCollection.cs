using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests
{
    [CollectionDefinition(CollectionName)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        public const string CollectionName = "DatabaseCollection";
    }
}