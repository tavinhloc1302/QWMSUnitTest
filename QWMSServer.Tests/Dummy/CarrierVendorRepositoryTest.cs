using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CarrierVendorRepositoryTest : RepositoryBaseTest<CarrierVendor>, ICarrierVendorRepository
    {
        public static int FLAG_DELETE = 0;
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
            var result = DataRecords.CARRIER_VENDOR_NORMAL;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.CARRIER_VENDOR_NORMAL_2);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.CARRIER_VENDOR_NORMAL);
                    break;
            }

            return result;
        }
    }
}
