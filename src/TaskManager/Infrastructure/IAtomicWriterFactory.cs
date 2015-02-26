namespace TaskManager.Infrastructure
{
    public interface IAtomicWriterFactory
    {
        IAtomicWriter<TView> GetProjectionWriter<TView>() where TView : class, new();
    }
}