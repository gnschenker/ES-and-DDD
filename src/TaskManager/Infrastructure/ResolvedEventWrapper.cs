using EventStore.ClientAPI;

namespace TaskManager.Infrastructure
{
    public class ResolvedEventWrapper : IResolvedEvent
    {
        public byte[] Metadata { get; set; }
        public byte[] Event { get; set; }
        public string EventType { get; set; }
        public Position? Position { get; set; }
    }
}