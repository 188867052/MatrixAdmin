using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public static class PagerExtension
    {
        /// <summary>
        /// IQueryable分页.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="pager">pager.</param>
        /// <returns></returns>
        public static IList<T> ToPagedList<T>(this IQueryable<T> query, Pager pager)
        {
            pager.TotalCount = query.Count();
            return query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
        }
    }
}