using System.Collections.Generic;

namespace TaskManager.Infrastructure
{
    public interface IProjectionRegistry
    {
        IEnumerable<object> GetMongoDbProjections(IAtomicWriterFactory factory);
    }
}