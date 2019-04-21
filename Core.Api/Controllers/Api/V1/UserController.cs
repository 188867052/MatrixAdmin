using System.Collections.Generic;
using System.Linq;
using Core.Api.Extensions;
using Core.Api.Models.Response;
using Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers11
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    //[CustomAuthorize]
    [Route("/user")]
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