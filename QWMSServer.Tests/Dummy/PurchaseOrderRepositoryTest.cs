using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PurchaseOrderRepositoryTest : RepositoryBaseTest<PurchaseOrder>, IPurchaseOrderRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<PurchaseOrder> GetObjectList()
        {
            return new List<PurchaseOrder>() {
                DataRecords.PURCHASEORDER_NORMAL,
                DataRecords.PURCHASEORDER_DELETED
            };
        }

        public override async Task<PurchaseOrder> GetAsync(Expression<Func<PurchaseOrder, bool>> where)
        {
            var result = DataRecords.PURCHASEORDER_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.PURCHASEORDER_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.PURCHASEORDER_NORMAL);
                    break;
            }
            return result;
        }
    }
}
