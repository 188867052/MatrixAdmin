using System;
using Core.Web.GridColumn;
using System.Collections.Generic;
using Core.Model;
using Core.Web.Html;

namespace Core.Web.ViewConfiguration
{
    public abstract class DialogConfiguration<T> : IRender
    {
        public abstract string Title { get; }
        public abstract string Footer { get; }

        public abstract string Body { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected DialogConfiguration(ResponseModel model)
        {
            this.GridColumn = new GridColumn<T>((List<T>)model.Data);
            this.Count = model.TotalCount;
            this.PageSize = model.PageSize;
            this.CurrentPage = model.PageIndex;
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public GridColumn<T> GridColumn { get; }
        public string Render()
        {
            throw new NotImplementedException();
        }
    }
}
