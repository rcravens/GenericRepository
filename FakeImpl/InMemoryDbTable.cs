using System.Collections.Generic;
using System.Linq;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class InMemoryDbTable<TKey, TEntity> where TEntity : class, IKeyed<TKey>
    {
        private readonly List<TEntity> _rows = new List<TEntity>();

        public bool Add(TEntity entity)
        {
            if (FindBy(entity.Id) == null)
            {
                _rows.Add(entity);
                return true;
            }
            return false;
        }

        public bool Update(TEntity entity)
        {
            if(Delete(entity.Id))
            {
                return Add(entity);
            }
            return false;
        }

        public bool Delete(TKey id)
        {
            TEntity toDelete = FindBy(id);
            if(toDelete!=null)
            {
                _rows.Remove(toDelete);
                return true;
            }
            return false;
        }

        public TEntity FindBy(TKey id)
        {
            return _rows.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public int Count()
        {
            return _rows.Count;
        }

        public IQueryable<TEntity> All()
        {
            return _rows.AsQueryable();
        }
    }
}