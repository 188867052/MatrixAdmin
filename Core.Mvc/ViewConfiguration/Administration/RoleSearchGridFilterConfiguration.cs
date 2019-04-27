﻿using Core.Model;
using Core.Mvc.ViewConfiguration.Log;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class RoleSearchGridFilterConfiguration : GridFilterConfiguration<RolePostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddTextFilter(new TextGridFilter<RolePostModel>(o => o.KeyWord, LogResource.Message));
       
            return GridSearchFilter.Render();
        }

        public override string GenerateButton()
        {
            this.Buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            this.Buttons.Add(new StandardButton("添加"));
            this.Buttons.Add(new StandardButton("编辑"));
            string html = default;
            string script = default;

            foreach (var button in Buttons)
            {
                html += button.Render();
                script += button.Event.Render();
            }

            script = $"<script>{script}</script>";
            return html + script;
        }
    }
}