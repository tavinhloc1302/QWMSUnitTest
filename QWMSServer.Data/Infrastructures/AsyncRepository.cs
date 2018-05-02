using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public class AsyncRepository<TEntity> : BaseRepository<TEntity>, IAsyncRepository<TEntity> where TEntity : class
    {
        public AsyncRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }



        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).FirstOrDefaultAsync<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null)
        {
            var query = this.Query(where, includes);
            if (query.Count() == 0)
                return null;
            return await query.Take(1).FirstAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null)
        {
            var query = this.Query(where, includes);
            return await query.ToListAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).CountAsync();
        }

    }
}
