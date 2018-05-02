using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public class SyncRepository<TEntity> : BaseRepository<TEntity>, ISyncRepository<TEntity> where TEntity : class
    {
        public SyncRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<TEntity>();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            var query = this.Query(where, includes);
            return query.FirstOrDefault<TEntity>(); // return query.Take(1).First();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            var query = this.Query(where, includes);
            return query.ToList();
        }
    }
}
