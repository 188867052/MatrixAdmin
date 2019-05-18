using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity
{
    public partial class CoreApiContext
    {
        public static CoreApiContext Instance = new CoreApiContext();

        public DbConnection Dapper => this.Database.GetDbConnection();
    }
}
