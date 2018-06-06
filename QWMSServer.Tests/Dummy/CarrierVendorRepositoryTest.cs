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
    }
}
