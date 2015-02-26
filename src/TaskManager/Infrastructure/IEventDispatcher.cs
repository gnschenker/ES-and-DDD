namespace TaskManager.Infrastructure
{
    public interface IEventDispatcher
    {
        void Dispatch(object e);
    }
}