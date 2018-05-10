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
    public class GatePassRepositoryTest : IGatePassRepository
    {
        public IQueryable<GatePass> Objects => throw new NotImplementedException();

        public void Add(GatePass entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<GatePass, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(GatePass entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<GatePass, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GatePass>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GatePass> GetAsync(Expression<Func<GatePass, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<GatePass> GetAsync(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<GatePass> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GatePass>> GetManyAsync(Expression<Func<GatePass, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GatePass>> GetManyAsync(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GatePass> Query(Expression<Func<GatePass, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(GatePass entity)
        {
            throw new NotImplementedException();
        }
    }
}
