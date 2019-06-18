using Core.Entity;
using Core.Model;
using Dapper;

namespace Core.Api.ControllerHelpers
{
    public static class UserControllerHelper
    {
        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted">isDeleted.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = @"UPDATE [User] SET is_deleted = @IsDeleted WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        ///  启用禁用用户.
        /// </summary>
        /// <param name="isEnable">IsEnable.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            string sql = @"UPDATE [User] SET is_enable = @IsEnable WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
