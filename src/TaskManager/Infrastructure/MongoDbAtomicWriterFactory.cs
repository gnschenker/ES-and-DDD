namespace TaskManager.Infrastructure
{
    public class MongoDbAtomicWriterFactory : IAtomicWriterFactory
    {
        private readonly string connectionString;
        private readonly string dbName;

        public MongoDbAtomicWriterFactory(string connectionString, string dbName)
        {
            this.connectionString = connectionString;
            this.dbName = dbName;
        }

        public IAtomicWriter<TView> GetProjectionWriter<TView>() where TView : class, new()
        {
            return new MongoDbAtomicWriter<TView>(connectionString, dbName);
        }
    }
}