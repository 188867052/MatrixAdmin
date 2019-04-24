using System;
using System.Linq.Expressions;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter : BaseGridFilter
    {
        public DateTimeGridFilter(string labelText) : base(labelText)
        {
            this.Delegate = "alert(this.value)";
            this.Event = new JavaScriptEvent(Delegate);
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }
    }
}