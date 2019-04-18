using Core.Repository;

namespace Core.Service
{
	public class Service : IService
	{
		private readonly IRepository _repository;
		public Service(IRepository repository)
		{
			this._repository = repository;
		}

		public string Test()
		{
			return this._repository.Test();
		}
	}
}
