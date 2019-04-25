using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.CustomException;
using Core.Api.Models.Response;
using Core.Model.Entity;
using Core.Model.PostModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ErrorController(Context dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this._dbContext)
            {
                IQueryable<Log> query = this._dbContext.Log.AsQueryable();
                var list = query.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult Search(LogPostModel model)
        {
            using (this._dbContext)
            {
                IQueryable<Log> query = this._dbContext.Log.AsQueryable();
                query = query.Where(o => o.Id.ToString().Contains(model.Id.ToString()));
                var list = query.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("{code}")]
        [HttpGet]
        public IActionResult Code(int code)
        {
            // 捕获状态码
            HttpStatusCode statusCode = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is HttpException httpEx ?
                httpEx.StatusCode : (HttpStatusCode)Response.StatusCode;
            HttpException ex = (HttpException)HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            HttpStatusCode parsedCode = (HttpStatusCode)code;
            ErrorDetails error = new ErrorDetails
            {
                StatusCode = code,
                Message = ex?.ToString()
            };
            // 如果是ASP.NET Core Web Api 应用程序，直接返回状态码(不跳转到错误页面，这里假设所有API接口的路径都是以/api/开始的)
            if (HttpContext.Features.Get<IHttpRequestFeature>().RawTarget.StartsWith("/api/", StringComparison.Ordinal))
            {
                parsedCode = (HttpStatusCode)code;
                // error = new ErrorDetails
                //{
                //    StatusCode = code,
                //    Message = parsedCode.ToString()
                //};

                return new ObjectResult(error);
            }
            IQueryable<Role> query = this._dbContext.Role.AsQueryable();
            List<Role> a = query.ToList();
            // error = new ErrorDetails
            //{
            //    StatusCode = code,
            //    Message = parsedCode.ToString()
            //};

            return new ObjectResult(a);
        }
    }
}