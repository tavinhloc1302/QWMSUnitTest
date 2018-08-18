using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    class TruckRepositoryTest : RepositoryBaseTest<Truck>, ITruckRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<Truck> GetObjectList()
        {
            return new List<Truck>() {
                DataRecords.TRUCK_NORMAL,
                DataRecords.TRUCK_DELETED,
                DataRecords.TRUCK_DELETED_TYPE,
            };
        }

        public override async Task<Truck> GetAsync(Expression<Func<Truck, bool>> where)
        {
            var result = DataRecords.TRUCK_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.TRUCK_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.TRUCK_NORMAL);
                    break;
            }
            return result; ;
        }
    }
}
