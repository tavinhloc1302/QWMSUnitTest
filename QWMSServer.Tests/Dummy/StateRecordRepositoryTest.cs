using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class StateRecordRepositoryTest : IStateRecordRepository
    {
        public IQueryable<StateRecord> Objects => new List<StateRecord>() {
                new StateRecord() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    gatePassID = 1,
                    stateID = 1,
                    stateStatus = 1                    
                },
                new StateRecord() {
                    code = "0123",
                    ID = 1,
                    isDelete = false,
                    gatePassID = 1,
                    stateID = 1,
                    stateStatus = 1
                }
            }.AsQueryable();

        public void Add(StateRecord entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<StateRecord, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(StateRecord entity)
        {
        }

        public void Delete(Expression<Func<StateRecord, bool>> where)
        {
        }

        public async Task<IEnumerable<StateRecord>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<StateRecord> GetAsync(Expression<Func<StateRecord, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<StateRecord> GetAsync(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<StateRecord> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<StateRecord>> GetManyAsync(Expression<Func<StateRecord, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<StateRecord>> GetManyAsync(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<StateRecord> Query(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(StateRecord entity)
        {
        }
    }
}
