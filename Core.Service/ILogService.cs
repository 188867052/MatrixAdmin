using System.Linq;
using Core.Repository.Dependency;

namespace Core.Service
{
    public interface ILogService : IDependency
    {
        void FindAll();
        void FindById(int id);
    }
}
