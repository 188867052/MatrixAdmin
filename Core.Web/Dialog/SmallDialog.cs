namespace Core.Web.Dialog
{
    public class SmallDialog
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\SmallDialog.html");

        public string Render()
        {
            return this.html;
        }
    }
}
