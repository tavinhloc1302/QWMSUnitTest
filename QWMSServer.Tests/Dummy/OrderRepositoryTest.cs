using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class OrderRepositoryTest : RepositoryBaseTest<Order>, IOrderRepository
    {
        public override IList<Order> GetObjectList()
        {
            return new List<Order>() {
                DataRecords.ORDER_NORMAL_DELI,
                DataRecords.ORDER_NORMAL_PURCHASE,
                DataRecords.ORDER_NORMAL_TYPE_OTHER,
            };
        }
    }
}
