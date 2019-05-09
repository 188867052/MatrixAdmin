using System;
using AutoMapper;
using Core.Api.Extensions;
using Core.Entity;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Authorize]
    public class MessageController : StandardController
    {
        /// <summary>
        /// 消息控制器.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Count()
        {
            return this.Ok(1);
        }

        /// <summary>
        /// 初始化消息标题列表.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Init()
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            object[] unread =
            {
                new {title="消息1",create_time=DateTime.Now,msg_id=1}
            };
            response.SetData(new { unread });
            return this.Ok(response);
        }

        /// <summary>
        /// 获取指定ID的消息内容.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{msgid:int}")]
        public IActionResult Content(int msgid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;

            response.SetData($"消息[{msgid}]内容");
            return this.Ok(response);
        }

        /// <summary>
        /// 将消息标为已读.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/message/has_read/{msgid}")]
        public IActionResult HasRead(int msgid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            return this.Ok(response);
        }

        /// <summary>
        /// 删除已读消息.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/message/remove_readed/{msgid}")]
        public IActionResult RemoveRead(int msgid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复已删消息.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/message/restore/{msgid}")]
        public IActionResult Restore(int msgid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            return this.Ok(response);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public MessageController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}