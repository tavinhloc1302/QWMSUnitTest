using QWMSServer.Data.Infrastructures;
using QWMSServer.Tests.Extensions;
using QWMSServer.Tests.Utils;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public abstract class RepositoryBaseTest<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected Random random = new Random();
        protected IList<TEntity> _ObjectList = null;
        public IQueryable<TEntity> Objects => _ObjectList.AsQueryable();

        // 0: Normal
        // Other: Exception
        public static int FLAG_ADD = 0;

        // 0: null
        // 1: Normal
        // 2: Deleted
        // Other: Exception
        public static int FLAG_GET_ASYNC = 0;
        public static int COUNT_GET_ASYNC = 1;
        public static int FLAG_GET_ASYNC_2 = 0;

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

        public static void ResetDummyFlags()
        {
            FLAG_ADD = 0;
            FLAG_GET_ASYNC = 0;
            COUNT_GET_ASYNC = 1;
            FLAG_GET_ASYNC_2 = 0;
        }

        public int GetFlagGet()
        {
            if (COUNT_GET_ASYNC > 1)
            {
                var nextPropName = "FLAG_GET_ASYNC_" + COUNT_GET_ASYNC.ToString();
                return ObjectUtils.GetProperty<int>(typeof(RepositoryBaseTest<TEntity>), nextPropName);
            }

            ++COUNT_GET_ASYNC;
            return FLAG_GET_ASYNC;
        }

        public void Add(TEntity entity)
        {
            //var biggestIdObj = this.Objects.OrderByDescending(o => ObjectUtils.GetProperty<int>(o, "ID")).First();
            //var biggestId = ObjectUtils.GetProperty<int>(biggestIdObj, "ID");
            //ObjectUtils.SetProperty(entity, "ID", biggestId + 1);

            //this.ObjectList.Add(entity);
            switch (FLAG_ADD)
            {
                case 0:
                    int randomId = (this.random.Next() % 1000000) + 1;
                    ObjectUtils.SetProperty(entity, "ID", randomId);
                    break;
                default:
                    throw new InvalidOperationException();
            }
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

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return this.Query(where).First();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            //return this.Objects.Filter(where.Compile()).First();
            //return this.Objects.Where(where).FirstOrDefault();
            return await this.GetAsync(where);
        }

        protected TEntity SimpleGetPatcher(TEntity obj)
        {
            switch (GetFlagGet())
            {
                case 0:
                    obj = null;
                    break;
                case 1:
                    break;
                case 2:
                    ObjectUtils.SetProperty(obj, "isDelete", true);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return obj;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return this.Objects.FindFirst((item) => ObjectUtils.GetProperty<int>(item, "ID") == id);
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            //return this.Query(where).ToList();
            var sampleEntity = await this.GetAsync(where);
            var resultList = new List<TEntity>();
            if (sampleEntity != null)
            {
                resultList.Add(sampleEntity);
            };

            return resultList;
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null)
        {
            //return this.Query(where, includes).ToList();
            return await this.GetManyAsync(where);
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
