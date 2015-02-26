using System;
using TaskManager.Contracts;
using TaskManager.Infrastructure;

namespace TaskManager.Domain
{
    public class TaskAggregate : IAggregate
    {
        private readonly TaskState state;

        public TaskAggregate(TaskState state = null)
        {
            this.state = state ?? new TaskState();
        }

        private void Apply(object @event)
        {
            state.Modify(@event);
        }

        public void Create(int id, string defaultName)
        {
            // check invariants
            if (state.Version != 0)
                throw new InvalidOperationException("Cannot re-create a task");

            // act
            Apply(new TaskCreated { Id = id, DefaultName = defaultName });
        }

        public void ChangeName(string newName)
        {
            if(state.Version == 0)
                throw new InvalidOperationException("Cannot operate on non-existing task");

            Apply(new TaskNameChanged{Id = state.Id, NewName = newName});
        }
    }
}