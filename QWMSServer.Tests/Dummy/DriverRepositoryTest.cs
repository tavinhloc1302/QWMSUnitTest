using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

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
    }
}
