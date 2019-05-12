using Microsoft.AspNetCore.Builder;

namespace Core.Mvc
{
    public class MyDeveloperExceptionPageOptions : DeveloperExceptionPageOptions
    {
        public MyDeveloperExceptionPageOptions()
        {
            this.SourceCodeLineCount = 8;
        }
    }
}