namespace Core.UnitTest.CodeGenerator
{
    public class Route
    {
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public string ControllerName { get; set; }
        public string Parameters { get; internal set; }
    }
}
