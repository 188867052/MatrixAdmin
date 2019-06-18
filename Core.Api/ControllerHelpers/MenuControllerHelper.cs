using Core.Entity;
using Core.Model;
using Dapper;

namespace Core.Api.ControllerHelpers
{
    public static class MenuControllerHelper
    {
        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted">isDeleted.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = @"UPDATE [Menu] SET IsDeleted = @IsDeleted WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="status">status.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateStatus(bool status, int[] ids)
        {
            string sql = @"UPDATE [Menu] SET Status = @Status WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { Status = status, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
