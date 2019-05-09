using Core.Model;

namespace Core.Api.Extensions
{
    /// <summary>
    /// /
    /// </summary>
    public class ResponseModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static ResponseModel CreateInstance => new ResponseModel();

        /// <summary>
        /// 
        /// </summary>
        public static ResponseModel CreateResultInstance => new ResponseModel();
    }
}
