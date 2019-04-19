namespace Core.Web.Dialog
{
    public class Table
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\Table.html");

        public Table()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
