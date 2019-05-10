using System.Linq;
using AutoMapper;
using Core.Entity;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        public readonly CoreApiContext DbContext;
        public readonly IMapper Mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        protected StandardController(CoreApiContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected IActionResult StandardResponse<T>(IQueryable<T> query, Pager pager)
        {
            pager.TotalCount = query.Count();
            if (pager.PageIndex < 1)
            {
                pager.PageIndex = 1;
            }

            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ResponseModel response = new ResponseModel(list, pager);

            return this.Ok(response);
        }

        protected IActionResult StandardResponse<T>(IQueryable<T> query)
        {
            Pager pager = Pager.CreateDefaultInstance();
            pager.TotalCount = query.Count();
            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ResponseModel response = new ResponseModel(list, pager);

            return this.Ok(response);
        }

        protected IActionResult StandardResponse<T>(DbSet<T> query) where T : class
        {
            Pager pager = Pager.CreateDefaultInstance();
            pager.TotalCount = query.Count();
            var list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
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