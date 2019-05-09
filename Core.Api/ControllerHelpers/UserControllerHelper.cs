using System.Data.Common;
using System.Data.SqlClient;
using Core.Api.Extensions;
using Core.Extension.Dapper;
using Core.Model;

namespace Core.Api.ControllerHelpers
{
    public static class UserControllerHelper
    {
        public static DbConnection Dapper = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = @"UPDATE [User] SET IsDeleted = @IsDeleted WHERE Id IN @Id";
            Dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="status">用户状态.</param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateStatus(bool status, int[] ids)
        {
            string sql = @"UPDATE [User] SET Status = @Status WHERE Id IN @Id";
            Dapper.Execute(sql, new { Status = status, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
