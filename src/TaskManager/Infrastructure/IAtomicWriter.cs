using System;

namespace TaskManager.Infrastructure
{
    public interface IAtomicWriter<TView> where TView : class
    {
        TView AddOrUpdate(int key, Func<TView> addFactory, Func<TView, TView> update, bool probablyExists = true);
        bool TryDelete(int key);
    }
}