using System;
using System.Linq;
using Core.Repository;

namespace Core.Service
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public void FindById(int id)
        {
        }

        public void FindAll()
        {
        }
    }
}
