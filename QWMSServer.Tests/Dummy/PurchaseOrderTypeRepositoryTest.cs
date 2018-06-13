using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QWMSServer.Tests.Dummy
{
    public class PurchaseOrderTypeRepositoryTest : RepositoryBaseTest<PurchaseOrderType>, IPurchaseOrderTypeRepository
    {
        public override IList<PurchaseOrderType> GetObjectList()
        {
            return new List<PurchaseOrderType>() {
            };
        }

        public override async Task<PurchaseOrderType> GetAsync(Expression<Func<PurchaseOrderType, bool>> where)
        {
            return null;
        }
    }
}
