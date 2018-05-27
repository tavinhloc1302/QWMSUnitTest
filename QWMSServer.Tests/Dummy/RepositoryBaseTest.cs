﻿using QWMSServer.Data.Infrastructures;
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
        protected IList<TEntity> _ObjectList = null;
        public IQueryable<TEntity> Objects => _ObjectList.AsQueryable();

        public RepositoryBaseTest()
        {
            this._ObjectList = this.GetObjectList();
        }

        public abstract IList<TEntity> GetObjectList();
        public void SetObjectList(IList<TEntity> value)
        {
            this._ObjectList = value;
        }

        protected IList<TEntity> ObjectList
        {
            get
            {
                return this.GetObjectList();
            }
            set
            {
                this.SetObjectList(value);
            }

        }

        public async void Add(TEntity entity)
        {
            var biggestIdObj = this.Objects.OrderByDescending(o => ObjectUtils.GetProperty<int>(o, "ID")).First();
            var biggestId = ObjectUtils.GetProperty<int>(biggestIdObj, "ID");
            ObjectUtils.SetProperty(entity, "ID", biggestId + 1);

            this.ObjectList.Add(entity);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return this.Query(where).Count();
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
            return this.Query(where).First();
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
            return this.Query(where).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Query(where, includes).ToList();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            var query = this.Objects.Where(where);
            if (includes != null)
                foreach (String name in includes)
                    query = query.Include(name);

            return query;
        }

        public async void Update(TEntity entity)
        {
            var found = false;
            var listIndex = 0;
            for (; listIndex < this.ObjectList.Count; listIndex++)
            {
                var curEntity = this.ObjectList[listIndex];
                if (ObjectUtils.GetProperty<int>(curEntity, "ID") == ObjectUtils.GetProperty<int>(entity, "ID"))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new KeyNotFoundException("No matching entity to update.");
            }

            this.ObjectList[listIndex] = entity;
        }
    }
}
