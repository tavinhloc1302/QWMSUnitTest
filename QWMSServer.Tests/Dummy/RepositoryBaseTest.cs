using QWMSServer.Data.Infrastructures;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using QWMSServer.Tests.Extensions;
using QWMSServer.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public abstract class RepositoryBaseTest<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        abstract public IQueryable<TEntity> Objects { get; }

        public async void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return this.Objects.Count();
        }

        public async void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async void Delete(Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            //return this.Objects.Filter(where.Compile()).First();
            return this.Objects.Where(where).FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return this.Objects.FindFirst((item) => ObjectUtils.GetProperty<int>(item, "ID") == id);
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public async void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
