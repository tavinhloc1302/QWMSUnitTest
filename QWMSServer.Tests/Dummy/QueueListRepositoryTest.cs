using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Tests.Dummy
{
    public class QueueListRepositoryTest : IQueueListRepository
    {
        public IQueryable<QueueList> Objects => throw new NotImplementedException();

        public void Add(QueueList entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<QueueList, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(QueueList entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<QueueList, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueList>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<QueueList> GetAsync(Expression<Func<QueueList, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<QueueList> GetAsync(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<QueueList> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueList>> GetManyAsync(Expression<Func<QueueList, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueList>> GetManyAsync(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<QueueList> Query(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(QueueList entity)
        {
            throw new NotImplementedException();
        }
    }
}
