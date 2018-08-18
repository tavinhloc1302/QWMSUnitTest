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
        public static int FLAG_DELETE = -1;

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
            var result = DataRecords.TRUCK_TYPE_TRUCK;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.TRUCK_TYPE_TRUCK);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.TRUCK_TYPE_CONTAINER);
                    break;
            }

            return result;
        }
    }
}
