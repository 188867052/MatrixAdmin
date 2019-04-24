using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    public class DropDownGridFilter : BaseGridFilter
    {
        public DropDownGridFilter(string labelText) : base(labelText)
        {
            this.Delegate = "alert(this.value)";
            this.Text = labelText;
            this.Event = new JavaScriptEvent(Delegate);
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }
        public string Text { get; set; }

        public override string Render()
        {
            return $"<label>{Text}</label>" +
                   $"<select>" +
                   $"<option>1</option>" +
                   $"<option>2</option>" +
                   $"<option>3</option>" +
                   $"<option>4</option>" +
                   $"<option>5</option>" +
                   $"</select>";
        }
    }
}