﻿using TaskManager.Infrastructure;

namespace TaskManager.Domain
{
    public interface ITaskApplicationService
    {
        CreateInfo When(CreateTaskCommand cmd);
    }

    public class TaskApplicationService : ITaskApplicationService
    {
        private readonly IRepository repository;
        private readonly IIdGenerator idGenerator;

        public TaskApplicationService(IRepository repository, IdGenerator idGenerator)
        {
            this.repository = repository;
            this.idGenerator = idGenerator;
        }

        public CreateInfo When(CreateTaskCommand cmd)
        {
            var id = idGenerator.Next<TaskAggregate>();
            var defaultName = "Task " + id;
            var agg = new TaskAggregate();
            agg.Create(id, defaultName);
            repository.Save(agg);
            return new CreateInfo{Id = id, DefaultName = defaultName};
        }
    }
}