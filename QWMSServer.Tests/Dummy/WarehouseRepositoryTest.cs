using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class WarehouseRepositoryTest : RepositoryBaseTest<Warehouse>, IWarehouseRepository
    {
        public static int FLAG_DELETE = 0;

        public override IList<Warehouse> GetObjectList()
        {
            return new List<Warehouse>() {
                DataRecords.WAREHOUSE_DELETED,
                DataRecords.WAREHOUSE_NORMAL
            };
        }

        public override async Task<Warehouse> GetAsync(Expression<Func<Warehouse, bool>> where)
        {
            var result = DataRecords.WAREHOUSE_NORMAL;

            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.WAREHOUSE_NORMAL);
                    break;
                default: // NO DELETE
                    result = this.SimpleGetPatcher(DataRecords.WAREHOUSE_DELETED);
                    break;
            }

            return result;
        }
    }
}
