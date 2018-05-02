using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Objects { get; }

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null);
    }
}
