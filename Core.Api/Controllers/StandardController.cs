using AutoMapper;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

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
        public StandardController(Context dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }
    }
}