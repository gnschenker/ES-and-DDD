using EventStore.ClientAPI;

namespace TaskManager.Infrastructure
{
    public interface IMongoDbEventPositionRepository
    {
        void Save(Position position);
        Position Get();
    }
}