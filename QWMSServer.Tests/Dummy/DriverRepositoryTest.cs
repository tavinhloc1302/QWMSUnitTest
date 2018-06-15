using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class DriverRepositoryTest : RepositoryBaseTest<Driver>, IDriverRepository
    {
        public override IList<Driver> GetObjectList()
        {
            return new List<Driver>() {
                DataRecords.DRIVER_NORMAL,
                DataRecords.DRIVER_DELETED,
            };
        }

        public override async Task<Driver> GetAsync(Expression<Func<Driver, bool>> where)
        {
            return new Driver()
            {
                code = "1234",
                carrierVendor = null,
                carrierVendorID = 1,
                ID = 1,
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                phoneNo = "0123456789",
                remark = "Remark",
                nameViNoSign = "Address in Vietnamese",
                driverLicenseNo = "0123456789",
                IDNo = "0123456789"
            };
        }
    }
}
