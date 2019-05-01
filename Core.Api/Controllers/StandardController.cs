using System.Linq;
using AutoMapper;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        public readonly Context DbContext;
        public readonly IMapper Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        protected StandardController(Context dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }


        protected IActionResult StandardResponse<T>(IQueryable<T> query, Pager pager)
        {
            pager.TotalCount = query.Count();
            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ResponseModel response = new ResponseModel(list, pager);

            return Ok(response);
        }

        protected IActionResult StandardResponse<T>(IQueryable<T> query)
        {
            Pager pager = Pager.CreateDefaultInstance();
            pager.TotalCount = query.Count();
            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ResponseModel response = new ResponseModel(list, pager);

            return Ok(response);
        }

        protected IActionResult StandardResponse<T>(DbSet<T> query) where T : class
        {
            Pager pager = Pager.CreateDefaultInstance();
            pager.TotalCount = query.Count();
            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ResponseModel response = new ResponseModel(list, pager);

            return Ok(response);
        }

        protected IActionResult SubmitResponse(ResponseModel response)
        {
            response.SetSuccess();
            return Ok(response);
        }
    }
}