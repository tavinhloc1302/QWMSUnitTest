using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CarrierVendorRepositoryTest : ICarrierVendorRepository
    {
        public IQueryable<CarrierVendor> Objects => new List<CarrierVendor>() {
                new CarrierVendor() {
                    addressEn = "Address in English",
                    addressVi = "Address in Vietnamese",
                    code = "0123",
                    contactPerson = "Galvin Nguyen",
                    department = "Sky",
                    ID = 1,
                    isDelete = false,
                    nameEn = "Sky Rider 1",
                    nameVi = "Sky Rider 1",
                    shortName = "SR1",
                    taxCode = "0123",
                    telNo ="0123456789"
                },
                   new CarrierVendor() {
                    addressEn = "Address in English",
                    addressVi = "Address in Vietnamese",
                    code = "3210",
                    contactPerson = "Galvin Nguyen",
                    department = "Sky",
                    ID = 2,
                    isDelete = false,
                    nameEn = "Sky Rider 2",
                    nameVi = "Sky Rider 2",
                    shortName = "SR2",
                    taxCode = "0123",
                    telNo ="98765432100123456789"
                }
            }.AsQueryable();

        public void Add(CarrierVendor entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(CarrierVendor entity)
        {
        }

        public void Delete(Expression<Func<CarrierVendor, bool>> where)
        {
        }

        public async Task<IEnumerable<CarrierVendor>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<CarrierVendor> GetAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<CarrierVendor> GetAsync(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<CarrierVendor> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<CarrierVendor>> GetManyAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<CarrierVendor>> GetManyAsync(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<CarrierVendor> Query(Expression<Func<CarrierVendor, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(CarrierVendor entity)
        {
        }
    }
}
