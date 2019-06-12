using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Entity;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Framework
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        protected StandardController(CoreContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected CoreContext DbContext { get; set; }

        protected IMapper Mapper { get; set; }

        protected IActionResult StandardResponse<T>(IQueryable<T> query, Pager pager)
        {
            if (pager.PageIndex < 1)
            {
                pager.PageIndex = 1;
            }

            var list = query.ToPagedList(pager);
            ResponseModel response = new ResponseModel(list, pager);

            return this.Ok(response);
        }

        protected IActionResult StandardSearchResponse<T, TResponse>(IQueryable<T> query, Pager pager, Func<T, TResponse> convert)
        {
            IList<TResponse> models = query.ToPagedList(pager).Select(convert).ToList();
            ResponseModel response = new ResponseModel(models, pager);
            return this.Ok(response);
        }

        protected IActionResult StandardResponse<T>(IQueryable<T> query)
        {
            Pager pager = Pager.CreateDefaultInstance();
            var list = query.ToPagedList(pager);
            ResponseModel response = new ResponseModel(list, pager);

            return this.Ok(response);
        }

        protected IActionResult StandardResponse<T>(DbSet<T> query) where T : class
        {
            Pager pager = Pager.CreateDefaultInstance();
            var list = query.ToPagedList(pager);
            ResponseModel response = new ResponseModel(list, pager);

            return this.Ok(response);
        }

        protected IActionResult SubmitResponse(ResponseModel response)
        {
            response.SetSuccess();
            return this.Ok(response);
        }
    }
}