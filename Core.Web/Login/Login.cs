namespace Core.Web.Dialog
{
    public class Login
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\Login.html");

        public Login()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
