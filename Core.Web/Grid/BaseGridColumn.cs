using System.Reflection;

namespace Core.Web.Grid
{
    public class BaseGridColumn<T>
    {
        public BaseGridColumn(PropertyInfo property, string thead)
        {
            this.PropertyInfo = property;
            this.Thead = thead;
        }

        public PropertyInfo PropertyInfo { get; set; }


        public string Thead { get; set; }

        public virtual string RenderTh()
        {
            return $"<th>{this.Thead}</th>"; ;
        }

        public virtual string RenderTd(T entity)
        {
            object value = this.PropertyInfo.GetValue(entity);
            return  $"<td>{value}</td>";
        }
    }
}