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
    public class StateRepositoryTest : IStateRepository
    {
        public IQueryable<State> Objects => throw new NotImplementedException();

        public void Add(State entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<State, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(State entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<State, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<State> GetAsync(Expression<Func<State, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<State> GetAsync(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<State> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetManyAsync(Expression<Func<State, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetManyAsync(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<State> Query(Expression<Func<State, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(State entity)
        {
            throw new NotImplementedException();
        }
    }
}
