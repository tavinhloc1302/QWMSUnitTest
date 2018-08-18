using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class SaleOrderRepositoryTest : RepositoryBaseTest<SaleOrder>, ISaleOrderRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<SaleOrder> GetObjectList()
        {
            return new List<SaleOrder>() {
                DataRecords.SALEORDER_NORMAL,
                DataRecords.SALEORDER_DELETED,
            };
        }

        public override async Task<SaleOrder> GetAsync(Expression<Func<SaleOrder, bool>> where)
        {
            var result = DataRecords.SALEORDER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.SALEORDER_NORMAL);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.SALEORDER_DELETED);
                    break;
            }
            return result;
        }
    }
}
