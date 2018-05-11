using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class DriverRepositoryTest : IDriverRepository
    {
        public IQueryable<Driver> Objects => new List<Driver>() {
            new Driver() {
                code = "0123",
                carrierVendor = new CarrierVendor(),
                carrierVendorID = 1,
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                phoneNo = "0123456789",
                remark = "Remark",
                nameViNoSign = "Address in Vietnamese",
                driverLicenseNo ="0123456789",
                IDNo = "0123456789"
            },
            new Driver() {
                code = "3210",
                carrierVendor = new CarrierVendor(),
                carrierVendorID = 2,
                ID = 2,
                isDelete = false,
                nameEn = "Sky Rider 2",
                nameVi = "Sky Rider 2",
                phoneNo = "9876543210",
                remark = "Remark",
                nameViNoSign = "Address in Vietnamese",
                driverLicenseNo ="9876543210",
                IDNo = "9876543210"
            },
        }.AsQueryable();

        public void Add(Driver entity)
        {

        }

        public async Task<int> CountAsync(Expression<Func<Driver, bool>> where)
        {
            return this.Objects.Count();
        }

        public void Delete(Driver entity)
        {
        }

        public void Delete(Expression<Func<Driver, bool>> where)
        {
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return this.Objects;
        }

        public async Task<Driver> GetAsync(Expression<Func<Driver, bool>> where)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Driver> GetAsync(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            return this.Objects.ElementAt(0);
        }

        public async Task<IEnumerable<Driver>> GetManyAsync(Expression<Func<Driver, bool>> where)
        {
            return this.Objects;
        }

        public async Task<IEnumerable<Driver>> GetManyAsync(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects;
        }

        public IQueryable<Driver> Query(Expression<Func<Driver, bool>> where, IEnumerable<string> includes = null)
        {
            return this.Objects.AsQueryable();
        }

        public void Update(Driver entity)
        {
        }
    }
}
