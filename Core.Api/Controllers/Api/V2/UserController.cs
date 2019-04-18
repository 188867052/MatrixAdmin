using System.Collections.Generic;
using System.Linq;
using Core.Api.Entities;
using Core.Api.Extensions;
using Core.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers.Api.V2
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    //[CustomAuthorize]
    [Route("api/v2/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _dbContext;
        public UserController(Context dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult List()
        {
            using (this._dbContext)
            {
                List<User> list = this._dbContext.User.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }
    }
}