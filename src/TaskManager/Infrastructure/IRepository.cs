namespace TaskManager.Infrastructure
{
    public interface IRepository
    {
        void Save(IAggregate aggregate);
    }
}