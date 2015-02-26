using System;
using System.Collections.Generic;
using TaskManager.Contracts;
using TaskManager.Infrastructure;

namespace TaskManager.Domain
{
    public class TaskAggregate : IAggregate
    {
        private readonly TaskState state;
        private readonly List<object> uncommittedEvents = new List<object>();

        public TaskAggregate(TaskState state = null)
        {
            this.state = state ?? new TaskState();
        }

        int IAggregate.Id { get { return state.Id; }}
        int IAggregate.Version { get { return state.Version; }}
        IEnumerable<object> IAggregate.GetUncommittedEvents() { return uncommittedEvents; }
        void IAggregate.ClearUncommittedEvents() { uncommittedEvents.Clear(); }

        private void Apply(object @event)
        {
            uncommittedEvents.Add(@event);
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