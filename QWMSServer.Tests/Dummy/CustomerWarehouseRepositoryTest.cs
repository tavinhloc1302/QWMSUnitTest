using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class CustomerWarehouseRepositoryTest : RepositoryBaseTest<CustomerWarehouse>, ICustomerWarehouseRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<CustomerWarehouse> GetObjectList()
        {
            return new List<CustomerWarehouse>() {
                DataRecords.CUSTOMERWAREHOUSE_DELETED,
                DataRecords.CUSTOMERWAREHOUSE_NORMAL
            };
        }

        public override async Task<CustomerWarehouse> GetAsync(Expression<Func<CustomerWarehouse, bool>> where)
        {
            var result = DataRecords.CUSTOMERWAREHOUSE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.CUSTOMERWAREHOUSE_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.CUSTOMERWAREHOUSE_DELETED);
                    break;
            }
            return result;
        }
    }
}
