namespace Core.Web.Dialog
{
    public class SmallDialog
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\SmallDialog.html");

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmallDialog()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
