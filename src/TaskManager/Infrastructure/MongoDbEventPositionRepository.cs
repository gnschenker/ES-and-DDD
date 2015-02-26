using EventStore.ClientAPI;
using MongoDB.Driver;

namespace TaskManager.Infrastructure
{
    public class MongoDbEventPositionRepository : IMongoDbEventPositionRepository
    {
        private readonly MongoCollection<LastProcessedPosition> collection;

        public MongoDbEventPositionRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(databaseName);
            collection = db.GetCollection<LastProcessedPosition>("LastProcessedPosition");
        }

        public void Save(Position position)
        {
            var lastProcessedPosition = new LastProcessedPosition
            {
                CommitPosition = position.CommitPosition,
                PreparePosition = position.PreparePosition
            };
            collection.Save(lastProcessedPosition);
        }

        public Position Get()
        {
            var item = collection.FindOne();
            if (item == null)
                return Position.Start;
            return new Position(item.CommitPosition, item.PreparePosition);
        }

        public class LastProcessedPosition
        {
            public int Id { get { return 1; } }
            public long CommitPosition { get; set; }
            public long PreparePosition { get; set; }
        }
    }
}