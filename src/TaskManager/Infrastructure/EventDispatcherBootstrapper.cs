using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace TaskManager.Infrastructure
{
    public class EventDispatcherBootstrapper
    {
        const string connectionString = "mongodb://localhost";
        const string connectionName = "EventDispatcher";
        private readonly IProjectionRegistry projectionRegistry;

        public EventDispatcherBootstrapper(IProjectionRegistry projectionRegistry)
        {
            this.projectionRegistry = projectionRegistry;
        }

        public void Initialize()
        {
            var logService = LogManager.GetLogger("Event Dispatcher");
            var eventStoreConnectionFactory = new EventStoreConnectionFactory();
            var mongoDbFactory = new MongoDbAtomicWriterFactory(connectionString, connectionName);
            var projections = projectionRegistry.GetMongoDbProjections(mongoDbFactory).ToArray();
            var repository = new MongoDbEventPositionRepository(connectionString, connectionName);
            var eventDispatcher = new EventDispatcher(projections, logService);
            var processor = new EventProcessor(eventStoreConnectionFactory, eventDispatcher, repository, logService);
            Task.Run(() => processor.Start(connectionName));
        }
    }
}