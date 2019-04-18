using Core.Repository.Dependency;

namespace Core.Service
{
	public interface IService : IDependency
	{
		string Test();
	}
}
