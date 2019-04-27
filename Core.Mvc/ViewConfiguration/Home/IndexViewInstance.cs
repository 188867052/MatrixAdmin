using Core.Web.JavaScript;

namespace Core.Mvc.ViewConfiguration.Home
{
    public class IndexViewInstance : IViewInstanceConstruction
    {
        public IndexViewInstance()
        {

        }

        public string InstanceClassName
        {
            get
            {
                return "Core";
            }
        }

        public JavaScript InitializeViewInstance()
        {
            JavaScript js = new JavaScript("core", "Core");
            return js;
        }
    }
}
