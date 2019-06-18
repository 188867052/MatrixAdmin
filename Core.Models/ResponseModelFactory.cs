namespace Core.Model
{
    /// <summary>
    /// ResponseModelFactory.
    /// </summary>
    public class ResponseModelFactory
    {
        /// <summary>
        /// CreateInstance.
        /// </summary>
        public static HttpResponseModel CreateInstance => new HttpResponseModel();

        /// <summary>
        /// CreateResultInstance.
        /// </summary>
        public static HttpResponseModel CreateResultInstance => new HttpResponseModel();
    }
}
