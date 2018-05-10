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
    public class StateRecordRepositoryTest : IStateRecordRepository
    {
        public IQueryable<StateRecord> Objects => throw new NotImplementedException();

        public void Add(StateRecord entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<StateRecord, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(StateRecord entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<StateRecord, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StateRecord>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StateRecord> GetAsync(Expression<Func<StateRecord, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<StateRecord> GetAsync(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<StateRecord> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StateRecord>> GetManyAsync(Expression<Func<StateRecord, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StateRecord>> GetManyAsync(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StateRecord> Query(Expression<Func<StateRecord, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(StateRecord entity)
        {
            throw new NotImplementedException();
        }
    }
}
