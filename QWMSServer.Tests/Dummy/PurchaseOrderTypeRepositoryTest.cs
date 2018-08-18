using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PurchaseOrderTypeRepositoryTest : RepositoryBaseTest<PurchaseOrderType>, IPurchaseOrderTypeRepository
    {
        public static int FLAG_DELETE = -1;

        public override IList<PurchaseOrderType> GetObjectList()
        {
            return new List<PurchaseOrderType>() {
                DataRecords.PURCHASEORDERTYPE_NORMAL,
                DataRecords.PURCHASEORDERTYPE_DELETED,
            };
        }

        public override async Task<PurchaseOrderType> GetAsync(Expression<Func<PurchaseOrderType, bool>> where)
        {
            var result = DataRecords.PURCHASEORDERTYPE_NORMAL;
            switch (FLAG_DELETE)
            {
                case 1: // No ID
                case 2: // Wrong ID
                    result = null;
                    break;
                case 0: // OK
                    result = this.SimpleGetPatcher(DataRecords.PURCHASEORDERTYPE_DELETED);
                    break;
                default:
                    result = this.SimpleGetPatcher(DataRecords.PURCHASEORDERTYPE_NORMAL);
                    break;
            }
            return result;
        }
    }
}
