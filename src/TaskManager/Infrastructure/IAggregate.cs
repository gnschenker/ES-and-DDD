using System.Collections.Generic;

namespace TaskManager.Infrastructure
{
    public interface IAggregate
    {
        int Id { get; }
        int Version { get; }
        IEnumerable<object> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}