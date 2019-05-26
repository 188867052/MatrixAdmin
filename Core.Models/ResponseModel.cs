using System.Net;

namespace Core.Model
{
    /// <summary>
    /// 请求响应实体.
    /// </summary>
    public class ResponseModel : Pager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModel"/> class.
        /// </summary>
        /// <param name="data">data.</param>
        /// <param name="pager">pager.</param>
        public ResponseModel(object data, Pager pager) : this()
        {
            this.Data = data;
            this.PageSize = pager.PageSize;
            this.PageIndex = pager.PageIndex;
            this.TotalCount = pager.TotalCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModel"/> class.
        /// 请求响应实体类.
        /// </summary>
        public ResponseModel()
        {
            this.Code = (int)HttpStatusCode.OK;
            this.Message = "操作成功";
        }

        /// <summary>
        /// 响应代码.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应消息内容.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的响应数据.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 设置响应状态为成功.
        /// </summary>
        /// <param name="message">message.</param>
        public void SetSuccess(string message = "成功")
        {
            this.Code = (int)HttpStatusCode.OK;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应状态为失败.
        /// </summary>
        /// <param name="message">message.</param>
        public void SetFailed(string message = "失败")
        {
            this.Message = message;
            this.Code = 999;
        }

        /// <summary>
        /// 设置响应状态为错误.
        /// </summary>
        /// <param name="message">message.</param>
        public void SetError(string message = "错误")
        {
            this.Code = (int)HttpStatusCode.InternalServerError;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应状态为未知资源.
        /// </summary>
        /// <param name="message">message.</param>
        public void SetNotFound(string message = "未知资源")
        {
            this.Code = (int)HttpStatusCode.NotFound;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应状态为无权限.
        /// </summary>
        /// <param name="message">message.</param>
        public void SetNoPermission(string message = "无权限")
        {
            this.Code = (int)HttpStatusCode.Unauthorized;
            this.Message = message;
        }

        /// <summary>
        /// 设置响应数据.
        /// </summary>
        /// <param name="data">data.</param>
        /// <param name="total">total.</param>
        public void SetData(object data, int total = 0)
        {
            this.Data = data;
            this.TotalCount = total;
        }
    }
}
