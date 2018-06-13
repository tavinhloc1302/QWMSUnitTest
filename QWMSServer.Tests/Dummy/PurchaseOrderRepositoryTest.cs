using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PurchaseOrderRepositoryTest : RepositoryBaseTest<PurchaseOrder>, IPurchaseOrderRepository
    {
        public override IList<PurchaseOrder> GetObjectList()
        {
            return new List<PurchaseOrder>() {
            };
        }

        public override async Task<PurchaseOrder> GetAsync(Expression<Func<PurchaseOrder, bool>> where)
        {
            return null;
        }
    }
}
