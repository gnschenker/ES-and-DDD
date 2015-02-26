using System.Collections.Generic;

namespace TaskManager.Infrastructure
{
    public interface IAggregateFactory
    {
        IAggregate Create<T>(IEnumerable<object> events) where T : IAggregate;
    }
}