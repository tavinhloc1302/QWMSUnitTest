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
        public static int FLAG_DELETE = -1;

        public override IList<Driver> GetObjectList()
        {
            return new List<Driver>() {
                DataRecords.DRIVER_NORMAL,
                DataRecords.DRIVER_DELETED,
            };
        }

        public override async Task<Driver> GetAsync(Expression<Func<Driver, bool>> where)
        {
            var result = DataRecords.DRIVER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.DRIVER_DELETED);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.DRIVER_NORMAL);
                    break;
            }
            return result;
        }
    }
}
