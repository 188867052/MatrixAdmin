using Core.Entity;
using Core.Model;
using Dapper;

namespace Core.Api.ControllerHelpers
{
    public static class LogControllerHelper
    {
        /// <summary>
        /// 清空.
        /// </summary>
        /// <returns>A ResponseModel.</returns>
        public static HttpResponseModel DeleteAll()
        {
            string sql = $"TRUNCATE TABLE [{nameof(Log)}]";
            CoreContext.Dapper.Execute(sql);
            return ResponseModelFactory.CreateInstance;
        }
    }
}
