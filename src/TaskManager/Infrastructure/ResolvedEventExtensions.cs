using EventStore.ClientAPI;

namespace TaskManager.Infrastructure
{
    public static class ResolvedEventExtensions
    {
        public static IResolvedEvent ToResolvedEventWrapper(this ResolvedEvent re)
        {
            return new ResolvedEventWrapper
            {
                EventType = re.Event.EventType,
                Event = re.Event.Data,
                Metadata = re.Event.Metadata,
                Position = re.OriginalPosition
            };
        }
    }
}