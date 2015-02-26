using EventStore.ClientAPI;

namespace TaskManager.Infrastructure
{
    public interface IResolvedEvent
    {
        byte[] Metadata { get; }
        byte[] Event { get; }
        string EventType { get; }
        Position? Position { get; }
    }
}