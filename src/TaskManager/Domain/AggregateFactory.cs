using System;
using System.Collections.Generic;
using TaskManager.Infrastructure;

namespace TaskManager.Domain
{
    public class AggregateFactory : IAggregateFactory
    {
        public IAggregate Create<T>(IEnumerable<object> events) where T : IAggregate
        {
            if (typeof(T) == typeof(TaskAggregate))
                return new TaskAggregate(new TaskState(events));

            throw new ArgumentException("Unknown aggregate type");
        }
    }
}