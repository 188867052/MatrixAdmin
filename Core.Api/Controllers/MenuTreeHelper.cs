using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Entity;
using Core.Entity.Enums;
using Core.Extension.Dapper;
using Core.Model;
using Core.Model.Administration.Menu;
using Microsoft.AspNetCore.Mvc;

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
            ILookup<Guid?, MenuTree> lookup = menus.ToLookup(x => x.ParentGuid);
            Func<Guid?, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree
                 {
                     Guid = x.Guid,
                     ParentGuid = x.ParentGuid,
                     Title = x.Title,
                     Expand = x.ParentGuid == null || x.ParentGuid == Guid.Empty,
                     Selected = selectedGuid == x.Guid,
                     Children = build(new Guid(x.Guid)),
                 })
                 .ToList();
            };
            List<MenuTree> result = build(null);
            return result;
        }
    }
}