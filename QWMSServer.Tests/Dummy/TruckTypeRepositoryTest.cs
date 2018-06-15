using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class TruckTypeRepositoryTest : RepositoryBaseTest<TruckType>, ITruckTypeRepository
    {
        public override IList<TruckType> GetObjectList()
        {
            return new List<TruckType>() {
                DataRecords.TRUCK_TYPE_TRUCK,
                DataRecords.TRUCK_TYPE_CONTAINER,
                DataRecords.TRUCK_TYPE_PUMP,
                DataRecords.TRUCK_TYPE_TRUCK_CONTAINER,
            };
        }

        public override async Task<TruckType> GetAsync(Expression<Func<TruckType, bool>> where)
        {
            return DataRecords.TRUCK_TYPE_TRUCK;
        }
    }
}
