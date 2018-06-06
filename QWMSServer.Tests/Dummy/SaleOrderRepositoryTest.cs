using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class SaleOrderRepositoryTest : RepositoryBaseTest<SaleOrder>, ISaleOrderRepository
    {
        public override IList<SaleOrder> GetObjectList()
        {
            return new List<SaleOrder>() {
            };
        }
    }
}
