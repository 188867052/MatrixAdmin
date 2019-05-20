using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Entity;
using Core.Extension.ExpressionBuilder.Generics;
using Core.Extension.FieldInfos;
using Core.Extension.Filters;
using Core.Model;
using Core.Model.Log;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class LogController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public LogController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                IQueryable<Log> query = this.DbContext.Log;
                query = query.OrderByDescending(o => o.CreateTime);
                return this.StandardResponse(query);
            }
        }

        [HttpPost]
        public IActionResult Search(LogPostModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Log> query = this.DbContext.Log;

                Filter<Log> filter = new Filter<Log>();
                filter.AddSimpleFilter(new IntegarEqualFilter<Log>(LogField.LogLevel, (int?)model.LogLevel));
                filter.AddSimpleFilter(new IntegarEqualFilter<Log>(LogField.SqlOperateType, (int?)model.SqlType));
                filter.AddSimpleFilter(new StringContainsFilter<Log>(LogField.Message, model.Message));
                filter.AddSimpleFilter(new DateTimeBetweenFilter<Log>(LogField.CreateTime, model.StartTime, model.EndTime));

                query = query.Where(filter);
                query = query.OrderByDescending(o => o.CreateTime);

                return this.StandardResponse(query, model);
            }
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Clear()
        {
            ResponseModel response = LogControllerHelper.DeleteAll();
            return this.Ok(response);
        }
    }
}