using System;

namespace TaskManager.Infrastructure
{
    public static class AtomicWriterExtensions
    {
        public static void Add<TItem>(this IAtomicWriter<TItem> writer, int key, Func<TItem> addFactory) where TItem : class
        {
            writer.AddOrUpdate(key, addFactory, null, false);
        }

        public static void Update<TItem>(this IAtomicWriter<TItem> writer, int key, Func<TItem, TItem> update) where TItem : class
        {
            writer.AddOrUpdate(key, null, update);
        }
    }
}