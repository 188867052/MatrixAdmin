using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class TextGridFilter : BaseGridFilter
    {
        public TextGridFilter(string label) : base(label)
        {
        }


        public string Event { get; set; }
    }
}