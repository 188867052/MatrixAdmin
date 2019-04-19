namespace Core.Web.Dialog
{
    public class LargeDialog
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\LargeDialog.html");

        public LargeDialog()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
