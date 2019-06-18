using System;
using Core.Entity;
using Core.Model;
using Dapper;

namespace Core.Api.ControllerHelpers
{
    public static class RoleControllerHelper
    {
        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isDeleted">isDeleted.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            string sql = $"UPDATE [Role] SET IsDeleted = @IsDeleted,{nameof(Role.UpdateTime)} =@UpdateTime  WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids, UpdateTime = DateTime.Now });
            return ResponseModelFactory.CreateInstance;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="isForbidden">status.</param>
        /// <param name="ids">ids.</param>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel UpdateIsForbidden(bool isForbidden, int[] ids)
        {
            string sql = $"UPDATE [Role] SET {nameof(Role.IsForbidden)} = @IsForbidden,{nameof(Role.UpdateTime)} =@UpdateTime WHERE Id IN @Id";
            CoreContext.Dapper.Execute(sql, new { IsForbidden = isForbidden, UpdateTime = DateTime.Now, Id = ids });
            return ResponseModelFactory.CreateInstance;
        }
    }
}
