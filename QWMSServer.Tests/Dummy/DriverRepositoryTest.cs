using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class DriverRepositoryTest : IDriverRepository
    {
        public IQueryable<Driver> Objects => throw new NotImplementedException();

        public void Add(Driver entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Driver, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(Driver entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Driver, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Driver>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Driver> GetAsync(Expression<Func<Driver, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<Driver> GetAsync(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<Driver> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Driver>> GetManyAsync(Expression<Func<Driver, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Driver>> GetManyAsync(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Driver> Query(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Driver entity)
        {
            throw new NotImplementedException();
        }
    }
}
