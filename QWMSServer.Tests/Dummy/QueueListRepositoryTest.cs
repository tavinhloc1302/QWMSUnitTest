using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class QueueListRepositoryTest : IQueueListRepository
    {
        public IQueryable<QueueList> Objects => new List<QueueList>() {
                new QueueList() {
                    estimateTime = 1,
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    gatePassID = 1,
                    laneID = 1,
                    truckID = 1,
                    queueTime = DateTime.Now,
                    queueOrder = 1
                },
                new QueueList() {
                    estimateTime = 1,
                    code = "3210",
                    ID = 2,
                    isDelete = false,
                    gatePassID = 2,
                    laneID = 2,
                    truckID = 2,
                    queueTime = DateTime.Now,
                    queueOrder = 2
                }
            }.AsQueryable();

        public void Add(QueueList entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<QueueList, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(QueueList entity)
        {
        }

        public void Delete(Expression<Func<QueueList, bool>> where)
        {
        }

        public async Task<IEnumerable<QueueList>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<QueueList> GetAsync(Expression<Func<QueueList, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<QueueList> GetAsync(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<QueueList> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<QueueList>> GetManyAsync(Expression<Func<QueueList, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<QueueList>> GetManyAsync(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<QueueList> Query(Expression<Func<QueueList, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(QueueList entity)
        {
        }
    }
}
