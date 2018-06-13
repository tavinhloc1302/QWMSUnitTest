using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;

using System.Collections.Generic;

namespace QWMSServer.Tests.Dummy
{
    public class OrderTypeRepositoryTest : RepositoryBaseTest<OrderType>, IOrderTypeRepository
    {
        public override IList<OrderType> GetObjectList()
        {
            return new List<OrderType>() {
                DataRecords.ORDER_TYPE_DELIVERY,
                DataRecords.ORDER_TYPE_PURCHASE,
                DataRecords.ORDER_TYPE_INTERNAL,
                DataRecords.ORDER_TYPE_OTHER,
            };
        }
    }
}
