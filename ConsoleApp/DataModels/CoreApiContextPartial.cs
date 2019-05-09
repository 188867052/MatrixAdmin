using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.DataModels
{
    public partial class CoreApiContext
    {
        public DbConnection Dapper => this.Database.GetDbConnection();
    }
}
