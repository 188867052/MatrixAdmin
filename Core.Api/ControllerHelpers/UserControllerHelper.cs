using System.Data.Common;
using System.Data.SqlClient;
using Core.Api.Extensions;
using Core.Extension.Dapper;
using Core.Model;

namespace Core.Api.ControllerHelpers
{
    public static class UserControllerHelper
    {
        private static DbConnection dapper = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted">isDeleted.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = @"UPDATE [User] SET IsDeleted = @IsDeleted WHERE Id IN @Id";
            dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="status">status.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateStatus(bool status, int[] ids)
        {
            string sql = @"UPDATE [User] SET Status = @Status WHERE Id IN @Id";
            dapper.Execute(sql, new { Status = status, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
