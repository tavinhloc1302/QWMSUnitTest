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
    public class CarrierVendorRepositoryTest : ICarrierVendorRepository
    {
        public IQueryable<CarrierVendor> Objects => throw new NotImplementedException();

        public void Add(CarrierVendor entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(CarrierVendor entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<CarrierVendor, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarrierVendor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CarrierVendor> GetAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<CarrierVendor> GetAsync(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<CarrierVendor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarrierVendor>> GetManyAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarrierVendor>> GetManyAsync(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CarrierVendor> Query(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(CarrierVendor entity)
        {
            throw new NotImplementedException();
        }
    }
}
