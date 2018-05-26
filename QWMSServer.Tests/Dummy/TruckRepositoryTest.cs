using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
    }
}
