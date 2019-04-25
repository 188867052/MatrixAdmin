namespace Core.Web.Dialog
{
    public class Carousel
    {
        private string html = System.IO.File.ReadAllText(@"C:\Users\54215\Desktop\Study\Asp.Net\Core.Web\File\Carousel.html");

        /// <summary>
        /// 构造函数
        /// </summary>
        public Carousel()
        {

        }

        public string Render()
        {
            return this.html;
        }
    }
}
