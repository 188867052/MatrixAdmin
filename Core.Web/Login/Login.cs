namespace Core.Web.Dialog
{
    //file:///C:/Users/54215/OneDrive/素材库/60套HTML5+CSS3后台管理登录模板/1649-60套HTML5+CSS3后台管理登录模板/045/grid.html#
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
