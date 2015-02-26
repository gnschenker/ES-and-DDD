namespace TaskManager.Infrastructure
{
    public interface IRepository
    {
        void Save(IAggregate aggregate);
        T GetById<T>(int id) where T : IAggregate;
    }
}