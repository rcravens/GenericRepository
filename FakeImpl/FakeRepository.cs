using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class FakeRepository<TKey, TEntity> : IKeyedRepository<TKey, TEntity> where TEntity : class, IKeyed<TKey>
    {
        private readonly InMemoryDbTable<TKey, TEntity> _table;

        public FakeRepository(InMemoryDbTable<TKey, TEntity> table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _table = table;
        }

        public bool Add(TEntity entity)
        {
            return _table.Add(entity);
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            bool result = true;
            foreach (TEntity entity in items)
            {
                result = Add(entity) && result;
            }
            return result;
        }

        public bool Update(TEntity entity)
        {
            return _table.Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            return _table.Delete(entity.Id);
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            bool result = true;
            foreach (TEntity entity in entities)
            {
                result = Delete(entity) && result;
            }
            return result;
        }

        public TEntity FindBy(TKey id)
        {
            return _table.FindBy(id);
        }

        public IQueryable<TEntity> All()
        {
            return _table.All();
        }

        public int Count()
        {
            return _table.Count();
        }
    }

}
