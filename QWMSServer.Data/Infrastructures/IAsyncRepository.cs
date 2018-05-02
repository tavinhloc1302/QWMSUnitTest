using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public interface IAsyncRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null);

        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);

        //Task<IPagedList<TEntity>> GetPageAsync<TOrder>(Page page, Expression<Func<TEntity, bool>> where);
    }
}
