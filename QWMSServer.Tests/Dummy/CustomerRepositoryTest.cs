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
    public class CustomerRepositoryTest : ICustomerRepository
    {
        public IQueryable<Customer> Objects => throw new NotImplementedException();

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Customer, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Customer, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetManyAsync(Expression<Func<Customer, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetManyAsync(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> Query(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
