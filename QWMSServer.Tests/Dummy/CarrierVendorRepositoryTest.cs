using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CarrierVendorRepositoryTest : RepositoryBaseTest<CarrierVendor>, ICarrierVendorRepository
    {
        public override IList<CarrierVendor> GetObjectList()
        {
            return new List<CarrierVendor>() {
                DataRecords.CARRIER_VENDOR_NORMAL,
                DataRecords.CARRIER_VENDOR_NORMAL_2,
                //DataRecords.CARRIER_VENDOR_DELETED,
            };
        }

        public override async Task<CarrierVendor> GetAsync(Expression<Func<CarrierVendor, bool>> where)
        {
            var sampleObject = new CarrierVendor()
            {
                ID = 1,
                addressEn = "Address in English",
                addressVi = "Address in Vietnamese",
                code = "0123",
                contactPerson = "Galvin Nguyen",
                department = "Sky",
                isDelete = false,
                nameEn = "Sky Rider 1",
                nameVi = "Sky Rider 1",
                shortName = "SR1",
                taxCode = "0123",
                telNo = "0123456789"
            };

            switch (FLAG_GET_ASYNC)
            {
                case 1:
                    sampleObject.isDelete = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return sampleObject;
        }
    }
}
