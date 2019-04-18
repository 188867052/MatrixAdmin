using Core.Repository.Dependency;

namespace Core.Repository
{
	public interface IRepository : IDependency
	{
		string Test();
	}
}