using QWMSServer.Data.Repository;
using QWMSServer.Model.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Dummy
{
    public class DeliveryOrderRepositoryTest : RepositoryBaseTest<DeliveryOrder>, IDeliveryOrderRepository
    {
        public override IList<DeliveryOrder> GetObjectList()
        {
            return new List<DeliveryOrder>() {
            };
        }
    }
}
