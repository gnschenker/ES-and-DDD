using System;
using TaskManager.Domain;
using TaskManager.Infrastructure;

namespace TaskManager.Controllers
{
    public class ApplicationServiceFactory
    {
        public T Create<T>()
        {
            if (typeof (T) == typeof (TaskApplicationService))
            {
                var idGenerator = new IdGenerator();
                var factory = new AggregateFactory();
                var connection = new EventStoreConnectionFactory().Create("TaskManager");
                var repository = new GesRepository(connection, factory);
                var service = new TaskApplicationService(repository, idGenerator);
                return (T)(object)service;
            }
            throw new InvalidOperationException("Unknown application service");
        }
    }
}