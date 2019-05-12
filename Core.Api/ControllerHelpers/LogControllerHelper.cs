using System.Data.Common;
using System.Data.SqlClient;
using Core.Entity;
using Core.Extension;
using Core.Extension.Dapper;
using Core.Model;

namespace Core.Api.ControllerHelpers
{
    public static class LogControllerHelper
    {
        private static readonly DbConnection dapper = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        /// <summary>
        /// 清空.
        /// </summary>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel DeleteAll()
        {
            string sql = $"TRUNCATE TABLE [{nameof(Log)}]";
            dapper.Execute(sql);
            return ResponseModelFactory.CreateInstance;
        }
    }
}
