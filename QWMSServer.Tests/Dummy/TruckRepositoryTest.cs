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
            return DataRecords.TRUCK_NORMAL;
        }
    }
}
