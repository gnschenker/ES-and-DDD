using System;
using System.Collections.Generic;
using TaskManager.Contracts;
using TaskManager.Infrastructure;

namespace TaskManager.Domain
{
    public class TaskState
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }

        public TaskState(IEnumerable<object> events = null)
        {
            if (events == null) return;
            foreach (var @event in events)
                Modify(@event);
        }

        private void When(TaskCreated e)
        {
            Id = e.Id;
            Name = e.DefaultName;
        }

        private void When(TaskNameChanged e)
        {
            Name = e.NewName;
        }

        public void Modify(object @event)
        {
            Version++;
            RedirectToWhen.InvokeEventOptional(this, @event);
        }
    }
}