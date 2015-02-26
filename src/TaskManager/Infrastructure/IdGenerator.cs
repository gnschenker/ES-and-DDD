namespace TaskManager.Infrastructure
{
    internal interface IIdGenerator
    {
        int Next<T>();
    }

    public class IdGenerator : IIdGenerator
    {
        public int Next<T>()
        {
            return -1;
        }
    }
}