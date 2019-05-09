namespace Core.Model
{

    public class Pager
    {
        public static int DefaultPageSize = 10;
        public static int DefaultPageIndex = 1;


        public static Pager CreateDefaultInstance()
        {
            Pager pager = new Pager
            {
                PageSize = Pager.DefaultPageSize,
                PageIndex = Pager.DefaultPageIndex,
            };

            return pager;
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 请求响应实体
    /// </summary>
    public class ResponseModel : Pager
    {

        public ResponseModel(object data, Pager pager) : this()
        {
            this.Data = data;
            this.PageSize = pager.PageSize;
            this.PageIndex = pager.PageIndex;
            this.TotalCount = pager.TotalCount;
        }

        /// <summary>
        /// 请求响应实体类
        /// </summary>
        public ResponseModel()
        {
            this.Code = 200;
            this.Message = "操作成功";
        }

        /// <summary>
        /// 响应代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的响应数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public void SetSuccess(string message = "成功")
        {
            this.Code = 200;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFailed(string message = "失败")
        {
            this.Message = message;
            this.Code = 999;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public void SetError(string message = "错误")
        {
            this.Code = 500;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public void SetNotFound(string message = "未知资源")
        {
            this.Message = message;
            this.Code = 404;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public void SetNoPermission(string message = "无权限")
        {
            this.Message = message;
            this.Code = 401;
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        public void SetData(object data, int total = 0)
        {
            this.Data = data;
            this.TotalCount = total;
        }
    }
}
