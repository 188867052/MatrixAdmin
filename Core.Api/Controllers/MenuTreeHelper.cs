using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model.Administration.Menu;

namespace Core.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public static class MenuTreeHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, string selectedGuid = null)
        {
            ILookup<int?, MenuTree> lookup = menus.ToLookup(x => x.ParentId);
            Func<int?, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree
                 {
                     Id = x.Id,
                     ParentId = x.ParentId,
                     Title = x.Title,
                     Expand = !x.ParentId.HasValue,
                     Selected = selectedGuid == x.Id.ToString(),
                     Children = build(x.Id)
                 })
                 .ToList();
            };
            List<MenuTree> result = build(null);
            return result;
        }
    }
}