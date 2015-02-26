using System.Collections.Generic;
using TaskManager.Infrastructure;

namespace TaskManager.ReadModel
{
    public class ProjectionRegistry : IProjectionRegistry
    {
        public IEnumerable<object> GetMongoDbProjections(IAtomicWriterFactory factory)
        {
            yield return new TaskProjection(factory.GetProjectionWriter<TaskView>());
        }
    }
}