namespace Core.Api.Models.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseResultModel : ResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public void SetData(object data, int totalCount = 0)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
