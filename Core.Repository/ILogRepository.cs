using Core.Repository.Dependency;

namespace Core.Repository
{
    public interface ILogRepository : IDependency
    {
        void FindAll();
    }
}