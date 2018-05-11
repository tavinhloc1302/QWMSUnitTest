using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CustomerRepositoryTest : ICustomerRepository
    {
        public IQueryable<Customer> Objects => new List<Customer>() {
            new Customer() {
                code = "0123",
                contactPerson = "Galvin Nguyen",
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                shortName = "SR1",
                taxCode = "0123",
                telNo ="0123456789",
                customerWarehouses = new List<CustomerWarehouse>(),
                email = "SkyRider1@qwms.com",
                faxNo = "0123456789",
                invoiceAddressEn = "Address in English",
                invoiceAddressVi = "Address in Vietnamese",
            },
            new Customer() {
                code = "3210",
                contactPerson = "Galvin Nguyen",
                ID = 2,
                isDelete = false,
                nameEn = "Sky Rider 2",
                nameVi = "Sky Rider 2",
                shortName = "SR2",
                taxCode = "0123",
                telNo ="9876543210",
                customerWarehouses = new List<CustomerWarehouse>(),
                email = "SkyRider2@qwms.com",
                faxNo = "9876543210",
                invoiceAddressEn = "Address in English",
                invoiceAddressVi = "Address in Vietnamese",
            }
        }.AsQueryable();

        public void Add(Customer entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Customer, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Customer entity)
        {
        }

        public void Delete(Expression<Func<Customer, bool>> where)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Customer>> GetManyAsync(Expression<Func<Customer, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Customer>> GetManyAsync(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Customer> Query(Expression<Func<Customer, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Customer entity)
        {
        }
    }
}
