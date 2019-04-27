using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.CustomException;
using Core.Model.Entity;
using Core.Model.PostModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Core.Model.ResponseModels;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LogController : StandardController
    {
        public LogController(Context dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                IQueryable<Log> query = this.DbContext.Log.AsQueryable();
                query = query.OrderByDescending(o => o.CreateTime);
                int count = query.Count();
                query = query.Paged();
                var list = query.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list, count);
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult Search(LogPostModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Log> query = this.DbContext.Log.AsQueryable();
                query = query.OrderByDescending(o => o.CreateTime);
                Log log = new Log
                {
                    Message = JsonConvert.SerializeObject(model),
                    CreateTime = DateTime.Now
                };
                this.DbContext.Log.Add(log);
                if (!string.IsNullOrEmpty(model.Message))
                {
                    query = query.Where(o => o.Message.Contains(model.Message));
                }

                this.DbContext.SaveChanges();

                query = query.Paged(model.CurrentPage, model.PageSize);

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
            IQueryable<Role> query = this.DbContext.Role.AsQueryable();
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