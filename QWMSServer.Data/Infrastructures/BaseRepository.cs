using QWMSServer.Data.Infrastructures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IDBContext _dbContext;
        protected readonly IDbSet<TEntity> _dbSet;

        public BaseRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Objects => _dbSet;

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = _dbSet.Where<TEntity>(where).AsEnumerable();
            foreach(TEntity obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            var query = _dbSet.Where(where);
            if (includes != null)
                foreach (String name in includes)
                    query = query.Include(name);

            return query;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
