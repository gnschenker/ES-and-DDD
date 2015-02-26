namespace TaskManager.Infrastructure
{
    public class GESRepository : IRepository
    {
        public void Save(IAggregate aggregate)
        {
            
        }

        public T GetById<T>(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}