using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Data.Infrastructures
{
    public interface ISyncRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        TEntity Get(Expression<Func<TEntity, bool>> where);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        TEntity Get(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, IEnumerable<String> includes = null);
    }
}
