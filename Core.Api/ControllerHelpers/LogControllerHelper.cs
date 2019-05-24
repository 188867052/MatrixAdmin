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
        public static ResponseModel DeleteAll()
        {
            string sql = $"TRUNCATE TABLE [{nameof(Log)}]";
            CoreApiContext.Dapper.Execute(sql);
            return ResponseModelFactory.CreateInstance;
        }
    }
}
