using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

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
    }
}
