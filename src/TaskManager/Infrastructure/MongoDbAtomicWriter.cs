using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TaskManager.Infrastructure
{
    public class MongoDbAtomicWriter<TView> : IAtomicWriter<TView> where TView : class
    {
        private readonly string connectionString;
        private readonly string databaseName;
        private readonly IDictionary<string, MongoDatabase> mongoDbs = new Dictionary<string, MongoDatabase>();

        public MongoDbAtomicWriter(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName.Replace(' ', '_').Replace('.', '_');
        }

        public TView AddOrUpdate(int id, Func<TView> addFactory, Func<TView, TView> update, bool probablyExists = true)
        {
            var collection = GetCollection();

            if (probablyExists == false)
            {
                var item = addFactory();
                collection.Insert(item);
                return item;
            }

            var query = Query.EQ("_id", new BsonInt32(id));
            var existingItem = collection.FindOneAs<TView>(query);

            if (existingItem == null)
            {
                if (addFactory == null)
                    throw new InvalidOperationException("Item does not exists and no add factory is defined");

                var newItem = addFactory();
                collection.Insert(newItem);
                return newItem;
            }

            var updatedItem = update(existingItem);
            collection.Save(updatedItem);
            return updatedItem;
        }

        public bool TryDelete(int id)
        {
            var collection = GetCollection();
            var query = Query.EQ("_id", new BsonInt32(id));
            var result = collection.Remove(query);
            return result.DocumentsAffected == 1;
        }

        private MongoCollection<TView> GetCollection()
        {
            var db = GetDatabase();
            var collection = db.GetCollection<TView>(typeof(TView).Name);
            return collection;
        }

        private MongoDatabase GetDatabase()
        {
            if (mongoDbs.ContainsKey(databaseName))
                return mongoDbs[databaseName];

            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var mongoDb = server.GetDatabase(databaseName);
            mongoDbs.Add(databaseName, mongoDb);
            return mongoDb;
        }
    }
}