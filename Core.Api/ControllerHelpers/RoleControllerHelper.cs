using System.Data.Common;
using System.Data.SqlClient;
using Core.Entity;
using Core.Extension;
using Core.Extension.Dapper;
using Core.Model;

namespace Core.Api.ControllerHelpers
{
    public static class RoleControllerHelper
    {
        private static readonly DbConnection dapper = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted">isDeleted.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = @"UPDATE [Role] SET IsDeleted = @IsDeleted WHERE Id IN @Id";
            dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isForbidden">status.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static ResponseModel UpdateIsForbidden(bool isForbidden, int[] ids)
        {
            string sql = $"UPDATE [Role] SET {nameof(Role.IsForbidden)} = @IsForbidden WHERE Id IN @Id";
            dapper.Execute(sql, new { IsForbidden = isForbidden, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
