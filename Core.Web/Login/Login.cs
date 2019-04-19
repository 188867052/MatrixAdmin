namespace Core.Web.Dialog
{
    public class Login
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Mvc\wwwroot\045\index.html");

        public Login()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
