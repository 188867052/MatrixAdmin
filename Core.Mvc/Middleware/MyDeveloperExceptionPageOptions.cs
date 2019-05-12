using Microsoft.AspNetCore.Builder;

namespace Core.Mvc.Middleware
{
    public class MyDeveloperExceptionPageOptions : DeveloperExceptionPageOptions
    {
        public MyDeveloperExceptionPageOptions()
        {
            this.SourceCodeLineCount = 8;
        }
    }
}