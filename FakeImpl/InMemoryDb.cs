using System;
using System.Collections.Generic;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class InMemoryDb
    {
        private readonly Dictionary<string , object> _tables = new Dictionary<string, object>();

        public InMemoryDbTable<TKey, TEntity> GetTable<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            InMemoryDbTable<TKey, TEntity> table;

            Type type = typeof (TEntity);
            Type key = typeof (TKey);
            string hash = type + key.ToString();
            if (_tables.ContainsKey(hash))
            {
                table = (InMemoryDbTable<TKey, TEntity>)_tables[hash];
            }
            else
            {
                table = new InMemoryDbTable<TKey, TEntity>();
                _tables[hash] = table;
            }

            return table;
        }

        public int TableCount()
        {
            return _tables.Count;
        }
    }
}