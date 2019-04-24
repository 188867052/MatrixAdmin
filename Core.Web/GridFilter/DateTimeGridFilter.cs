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
        //$(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd hh:ii' });
        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }

        public override string Render()
        {
            return $"<div class=\"custom-control-inline\">" +
                   $"<label>{this.Value}</label>" +
                  $"<input size=\"16\" type=\"text\" value=\"2012-06-15 14:45\" readonly class=\"form_datetime\">" +
                   $"</div>";
            //return "<input size=\"16\" type=\"text\" value=\"2012-06-15 14:45\" readonly class=\"form_datetime\">";
        }

    }
}