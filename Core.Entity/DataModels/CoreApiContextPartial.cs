using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity.DataModels
{
    public partial class CoreApiContext
    {
        public DbConnection Dapper => this.Database.GetDbConnection();
    }
}
